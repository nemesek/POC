using CompositionAndDelegation.Better;

namespace CompositionAndDelegation
{
    public class Program
    {
        public void Main(string[] args)
        {
            DemoCarNoDelegation();
            DemoHouseUsingCar();
            DemoCarWithDelegation();
            DemoNewHouse();
            DemoNewHouseWithStereo();
        }

        public void DemoCarNoDelegation()
        {
            var car = new CarWithNoDelegation();
            car.Drive("Florida");
            car.ToggleRadio();
            car.ChangeStation("Alternative");

        }

        public void DemoHouseUsingCar()
        {
            var house = new House();
            house.Clean();
            house.PlayMusic("Classical");
        }

        public void DemoCarWithDelegation()
        {
            var car = new CarWithDelegation();
            car.Drive("Hawaii");
            car.ToggleRadio();
            car.ChangeStation("Alternative");
        }

        public void DemoNewHouse()
        {
            var house = new Better.House(new Radio());
            house.Clean();
            house.PlayMusic("Classical");
        }

        public void DemoNewHouseWithStereo()
        {
            var house = new Better.House(new Stereo());
            house.Clean();
            house.PlayMusic("Rock");
        }
    }
}
