using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
// from http://www.udidahan.com/2009/06/14/domain-events-salvation/
namespace DomainEvents
{
	public static class DomainEvents
	{
	    [ThreadStatic] //so that each thread has its own callbacks
	    private static List<Delegate> _actions;// = new List<delegate>();
		
		//public static IContainer Container {get; set;}
		
		// Registers a callback for the given domain event
		public static void Register<T>(Action<T> callback) // where T : IDomainEvent
		{
			if(_actions == null) _actions = new List<Delegate>();
			
			_actions.Add(callback);
		}
		
		//Clears callbacks passed to Register on the current thread
       public static void ClearCallbacks ()
       {
           _actions = null;
       }
	   
	   //Raises the given domain event
       public static void Raise<T>(T args) //where T : IDomainEvent
       {
            //           if (Container != null)
            // 		  {
            //            	foreach(var handler in Container.ResolveAll<Handles<T>>())
            //                 {
            // 					handler.Handle(args);
            // 				}
            // 		  }


            if (_actions == null) return;
            foreach (var action1 in _actions.Select(action => action as Action<T>))
            {
                action1?.Invoke(args);
            }
        }
	}
}