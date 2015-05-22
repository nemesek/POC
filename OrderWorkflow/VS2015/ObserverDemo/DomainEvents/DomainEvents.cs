using System;
using System.ComponentModel;
using System.Collections.Generic;
// from http://www.udidahan.com/2009/06/14/domain-events-salvation/
namespace DomainEvents
{
	public static class DomainEvents
	{
		[ThreadStatic] //so that each thread has its own callbacks
		private static List<delegate> _actions = new List<delegate>();
		
		public static IContainer Container {get; set;}
		
		// Registers a callback for the given domain event
		public static void Register<T>(Action<T> callback) // where T : IDomainEvent
		{
			//if(_actions == null) _actions = new List<delegate>();
			
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

     
          if (_actions != null)
		  {
              foreach (var action in _actions)
			  {
                	if (action is Action<T>) ((Action<T>)action)(args);	
			  }
  
		  }

      }

	}
}