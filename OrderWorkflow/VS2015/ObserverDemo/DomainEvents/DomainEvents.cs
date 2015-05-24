using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// from http://www.udidahan.com/2009/06/14/domain-events-salvation/
namespace DomainEvents
{
	public static class DomainEvents
	{
	    [ThreadStatic] //so that each thread has its own callbacks
	    private static List<Delegate> _actions;// = new List<delegate>();

	    private static List<Func<IDomainEvent,bool>> _funcs;
	    //private static List<Task<bool>> _tasks;
		
		//public static IContainer Container {get; set;}
		
		// Registers a callback for the given domain event
		public static void Register<T>(Action<T> callback)  where T : IDomainEvent
		{
			if(_actions == null) _actions = new List<Delegate>();
			
			_actions.Add(callback);
		}

        public static void Register(Func<IDomainEvent, bool> callback)
        {
            if (_funcs == null) _funcs = new List<Func<IDomainEvent,bool>>();

            _funcs.Add(callback);
        }

        //Clears callbacks passed to Register on the current thread
        public static void ClearCallbacks ()
       {
           _actions = null;
       }
	   
	   //Raises the given domain event
       public static void Raise<T>(T args) where T : IDomainEvent
       {
            //           if (Container != null)
            // 		  {
            //            	foreach(var handler in Container.ResolveAll<Handles<T>>())
            //                 {
            // 					handler.Handle(args);
            // 				}
            // 		  }


            if (_actions == null) return;
            foreach (var a in _actions.Select(action => action as Action<T>))
            {
                a?.Invoke(args);
                
            }
        }

        public static async Task<bool> RaiseAsync(IDomainEvent args)
        {
            // if (_actions == null) return false;
            var funcs = _funcs
                .Select(action => action as Func<IDomainEvent, bool>)
                .Where(a => a != null)
                .Select(Asyncify);

            await Task.WhenAll(funcs);
      
            //return Task.FromResult<bool>(true);
            return true;
        }

	    private static Task<Func<IDomainEvent, bool>> Asyncify(Func<IDomainEvent, bool> func)
	    {
	        return Task.FromResult(func);
	    }
	}
}