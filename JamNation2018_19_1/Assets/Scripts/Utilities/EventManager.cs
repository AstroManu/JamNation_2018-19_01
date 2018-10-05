using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager{

	public float timeRef						{ get; private set; }
	public float fixedTimeRef					{ get; private set; }

	public List<IGameEvent> timedEvents			{ get; private set; }
	public List<IGameEvent> updateEvents		{ get; private set; }
	public List<IGameEvent> fixedUpdateEvents	{ get; private set; }
	
	public EventManager ()
	{
		timeRef				= 0f;
		fixedTimeRef		= 0f;
		timedEvents			= new List<IGameEvent>();
		updateEvents		= new List<IGameEvent>();
		fixedUpdateEvents	= new List<IGameEvent>();
	}

	public void Update(float deltaTime)
	{

		timeRef					+= deltaTime;
		UpdateTimedEvents		(timedEvents, timeRef, deltaTime);	
		UpdateContinuousEvents	(updateEvents, timeRef, deltaTime);
	}
	public void FixedUpdate(float fixedDeltaTime)
	{
		fixedTimeRef += fixedDeltaTime;
		UpdateContinuousEvents(fixedUpdateEvents, fixedTimeRef, fixedDeltaTime);
	}

	private void UpdateTimedEvents (List<IGameEvent> _events, float _time, float deltaTime)
	{
		//TODO : Update only expired events, and sort the rest
		int i = 0;
		while (i <= _events.Count - 1)
		{
			if (_events[i].markForDelete)
				{ _events[i].EventRemove(_time, deltaTime); _events.RemoveAt(i); }
			else if (_time >= _events[i].triggerTime)
				{ _events[i].EventUpdate(_time, deltaTime); i++; }
			else
				{ i++; }
		}
	}

	private void UpdateContinuousEvents (List<IGameEvent> _events, float _time, float deltaTime)
	{
		int i = 0;
		while (i <= _events.Count - 1)
		{
			if (_events[i].markForDelete)
				{ _events[i].EventRemove(_time, deltaTime); _events.RemoveAt(i); }
			else
				{ _events[i].EventUpdate(_time, deltaTime); i++; }
		}
	}

	#region Add Events methods
	public TimedEvent AddTimedEvent(TimedEvent.eventCallback n_callback, TimedEvent.eventCallback n_removalCall, float n_delay, string n_tag = "")
	{
		TimedEvent newEvent = new TimedEvent(this, n_callback, n_removalCall, timeRef, timeRef + n_delay, n_tag);
		timedEvents.Add(newEvent);
		
		return newEvent;
	}
	public void AddTimedEvent(IGameEvent n_customEvent)
	{
		timedEvents.Add(n_customEvent);
	}

	public TimedMultipleEvent AddTimedRepeatingEvent(TimedMultipleEvent.eventCallback n_callback, TimedMultipleEvent.eventCallback n_removalCall, float n_delays, int n_repeatAmount, string n_tag = "")
	{
		//--- Validity check
		if (n_delays <= 0f || n_repeatAmount < 1) { return null; }
		//---

		float delay				= timeRef;
		float[] triggerTimes	= new float[n_repeatAmount];
		for (int i = 0; i < n_repeatAmount; i++)
			{ delay += n_delays; triggerTimes[i] = delay; }

		TimedMultipleEvent newEvent = new TimedMultipleEvent(this, n_callback, n_removalCall, timeRef, triggerTimes, n_tag);
		timedEvents.Add(newEvent);

		return newEvent;
	}
	public TimedMultipleEvent AddTimedMultipleEvents(TimedMultipleEvent.eventCallback n_callback, TimedMultipleEvent.eventCallback n_removalCall, float[] n_delays, string n_tag = "")
	{
		//--- Validity check
		if (n_delays.Length < 1 || n_delays == null) { return null; }
		//---

		for (int i = 0; i < n_delays.Length; i++)
			{ n_delays[i] = n_delays[i] + timeRef; }

		TimedMultipleEvent newEvent = new TimedMultipleEvent(this, n_callback, n_removalCall, timeRef, n_delays, n_tag);
		timedEvents.Add(newEvent);
		
		return newEvent;
	}

	public ContinuousTimedEvent AddContinuousEvent(bool _inFixedUpdate, ContinuousTimedEvent.eventCallback n_callback, ContinuousTimedEvent.eventCallback n_removalCall, float n_duration, string n_tag = "")
	{
		//--- Validity check
		if (n_duration <= 0f) { return null; }
		//---

		ContinuousTimedEvent newEvent = null;
		if (_inFixedUpdate)
		{
			newEvent = new ContinuousTimedEvent(this, n_callback, n_removalCall, fixedTimeRef, fixedTimeRef + n_duration, n_tag);
			fixedUpdateEvents.Add(newEvent);
		}
		else
		{
			newEvent = new ContinuousTimedEvent(this, n_callback, n_removalCall, timeRef, timeRef + n_duration, n_tag);
			updateEvents.Add(newEvent);
		}

		return newEvent;
	}
	public void AddContinuousEvent(IGameEvent n_customEvent, bool _inFixedUpdate)
	{
		if (_inFixedUpdate)
		{
			fixedUpdateEvents.Add(n_customEvent);
		}
		else
		{
			updateEvents.Add(n_customEvent);
		}
	}

	public EventDelayedTrigger AddDelayedContinuousEvent (bool _inFixedUpdate, ContinuousTimedEvent.eventCallback n_callback, ContinuousTimedEvent.eventCallback n_removalCall, float n_delay, float n_duration, string n_tag = "")
	{
		//--- Validity check
		if (n_delay <= 0f || n_duration <= 0f) { return null; }
		//---

		float time = _inFixedUpdate ? fixedTimeRef : timeRef;
		ContinuousTimedEvent	newEvent	= new ContinuousTimedEvent	(this, n_callback, n_removalCall, time + n_delay, time + n_duration + n_delay, n_tag);
		EventDelayedTrigger		newTrigger	= new EventDelayedTrigger	(this, newEvent, _inFixedUpdate ? fixedUpdateEvents : updateEvents, time, time + n_delay);
		
		timedEvents.Add(newTrigger);
		return newTrigger;
	}
	public EventDelayedTrigger AddDelayedContinuousEvent (bool _inFixedUpdate, IGameEvent n_customEvent, float n_delay)
	{
		//--- Validity check
		if (n_delay <= 0f) { return null; }
		//---

		float time = _inFixedUpdate ? fixedTimeRef : timeRef;
		EventDelayedTrigger newTrigger = new EventDelayedTrigger(this, n_customEvent, _inFixedUpdate ? fixedUpdateEvents : updateEvents, time, time + n_delay);

		timedEvents.Add(newTrigger);
		return newTrigger;
	}
	#endregion
}

public interface IGameEvent
{
	EventManager	eventManager		{ get; set; }
	float			startTime			{ get; set; }
	float			triggerTime			{ get; set; }
	string			eventTag			{ get; set; }
	bool			markForDelete		{ get; set; }
	
	void			EventUpdate(float time, float deltaTime);
	void			EventRemove(float time, float deltaTime);
	float			RemainingDuration	{ get; }
	float			Progress			{ get; }
}

#region Timed Events
public class TimedEvent : IGameEvent
{
	public EventManager	eventManager	{ get; set; }
	public float		startTime		{ get; set; }
	public float		triggerTime		{ get; set; }
	public string		eventTag		{ get; set; }
	public bool			markForDelete	{ get; set; }

	public delegate void eventCallback(TimedEvent gameEvent);
	public eventCallback callback;
	public eventCallback removalCall;

	public TimedEvent (EventManager _eventManager, eventCallback methodCall, eventCallback removalMethod, float _startTime, float n_triggerTime, string n_tag)
	{
		eventManager	= _eventManager;
		markForDelete	= false;
		startTime		= _startTime;
		triggerTime		= n_triggerTime;
		eventTag		= n_tag;
		callback		= methodCall;
		removalCall		= removalMethod;
	}

	public void EventUpdate(float time, float deltaTime)
	{
		callback(this);
		markForDelete = true;
	}

	public void EventRemove(float time, float deltaTime)
	{
		if (removalCall != null) { removalCall(this); }
	}

	public float RemainingDuration
	{ get { return triggerTime - eventManager.timeRef; }}

	public float Progress
	{ get { return 1f - (triggerTime - eventManager.timeRef) / (triggerTime - startTime); }}
}

public class TimedMultipleEvent : IGameEvent
{
	public EventManager eventManager	{ get; set; }
	public float		startTime		{ get; set; }
	public float		triggerTime		{ get; set; }
	public string		eventTag		{ get; set; }
	public bool			markForDelete	{ get; set; }

	public float[]		triggerTimes	{ get; set; }
	public int			i				{ get; set; }

	public delegate void eventCallback(TimedMultipleEvent gameEvent);
	public eventCallback callback;
	public eventCallback removalCall;

	public TimedMultipleEvent (EventManager _eventManager, eventCallback methodCall, eventCallback removalMethod, float _startTime, float[] n_triggerTimes, string n_tag)
	{
		eventManager	= _eventManager;
		markForDelete	= false;
		i				= 0;
		startTime		= _startTime;
		triggerTimes	= n_triggerTimes;
		triggerTime		= triggerTimes[i];
		eventTag		= n_tag;
		callback		= methodCall;
		removalCall		= removalMethod;
	}

	public void EventUpdate(float time, float deltaTime)
	{
		callback(this);
		i++;
		if (i < triggerTimes.Length)	{ triggerTime = triggerTimes[i]; }
		else							{ markForDelete = true; }
	}

	public void EventRemove(float time, float deltaTime)
	{
		if (removalCall != null) { removalCall(this); }
	}

	public float RemainingDuration
	{ get{ return triggerTimes[triggerTimes.Length - 1] - startTime; }}

	public float Progress
	{ get{ return 1f - (triggerTimes[triggerTimes.Length - 1] - eventManager.timeRef) / (triggerTimes[triggerTimes.Length - 1] - startTime); }}
}
#endregion

#region Continuous Events
public class ContinuousTimedEvent : IGameEvent
{
	public EventManager eventManager	{ get; set; }
	public float		startTime		{ get; set; }
	public float		triggerTime		{ get; set; }
	public string		eventTag		{ get; set; }
	public bool			markForDelete	{ get; set; }

	public delegate void eventCallback(ContinuousTimedEvent gameEvent, float deltaTime);
	public eventCallback callback;
	public eventCallback removalCall;

	public ContinuousTimedEvent (EventManager _eventManager, eventCallback methodCall, eventCallback removalMethod, float _startTime, float n_triggerTime, string n_tag)
	{
		eventManager	= _eventManager;
		markForDelete	= false;
		startTime		= _startTime;
		triggerTime		= n_triggerTime;
		eventTag		= n_tag;
		callback		= methodCall;
		removalCall		= removalMethod;
	}

	public void EventUpdate(float time, float deltaTime)
	{
		callback(this, deltaTime);
		markForDelete = time >= triggerTime || markForDelete;
	}

	public void EventRemove(float time, float deltaTime)
	{
		if (removalCall != null) { removalCall(this, deltaTime); }
	}

	public float RemainingDuration
	{ get { return triggerTime - eventManager.timeRef; } }

	public float Progress
	{ get { return 1f - (triggerTime - eventManager.timeRef) / (triggerTime - startTime); } }
}
public class EventDelayedTrigger : IGameEvent
{
	public EventManager	eventManager	{ get; set; }
	public float		startTime		{ get; set; }
	public float		triggerTime		{ get; set; }
	public string		eventTag		{ get; set; }
	public bool			markForDelete	{ get; set; }
	
	public IGameEvent		delayedEvent	{ get; private set; }
	public List<IGameEvent>	targetList		{ get; private set; }

	public EventDelayedTrigger (EventManager _eventManager, IGameEvent n_delayedEvent, List<IGameEvent> _targetList, float _startTime, float n_triggerTime)
	{
		eventManager	= _eventManager;
		markForDelete	= false;
		delayedEvent	= n_delayedEvent;
		targetList		= _targetList;
		startTime		= _startTime;
		triggerTime		= n_triggerTime;
	}

	public void EventUpdate(float time, float deltaTime)
	{
		targetList.Add(delayedEvent);
		markForDelete	= true;
	}

	public void EventRemove(float time, float deltaTime)
	{
		//Do nothing
	}

	public float RemainingDuration
	{ get { return triggerTime - eventManager.timeRef; } }

	public float Progress
	{ get { return 1f - (triggerTime - eventManager.timeRef) / (triggerTime - startTime); } }
}
#endregion