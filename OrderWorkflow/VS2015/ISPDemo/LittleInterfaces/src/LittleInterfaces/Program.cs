using System;

namespace LittleInterfaces
{
    public class Program
    {
        public void Main(string[] args)
        {
            var cms = new Cms(new AutoAssignService());
            cms.AssignOrder(1);
            Console.WriteLine("Done");
        }
    }
}
