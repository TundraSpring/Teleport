using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleport
{
    public partial class FlowerLibrary : Library<Dygnflower>
    {
        private static FlowerLibrary _instance;
        public static FlowerLibrary Instance => _instance;
        public override void _EnterTree()
        {
            if (_instance != null)
            {
                this.QueueFree();
            }
            _instance = this;
        }






        //public Dictionary<int, List<Dygnflower>> flowerLibrary = new Dictionary<int, List<Dygnflower>>();

        //public override void _Ready()
        //{
        //    Dygnflower dygnflower1 = new Dygnflower();
        //    Dygnflower dygnflower2 = new Dygnflower();

        //    dygnflower1.SetData("A01-R01-F01", 1, DygnflowerStatus.Sleeping, DygnflowerType.Night, new Vector2(-9452, -16), false);
        //    dygnflower2.SetData("A01-R01-F02", 1, DygnflowerStatus.Sleeping, DygnflowerType.Night, new Vector2(-1180, -99), false);

        //    List<Dygnflower> dygnflowers1 = new List<Dygnflower> { dygnflower1, dygnflower2 };

        //    flowerLibrary.Add(1, dygnflowers1);
        //}

        //public void UpdateFlowerData(int channel, List<Dygnflower> dygnFlowers)
        //{
        //    flowerLibrary[channel] = dygnFlowers;
        //}

        //public List<Dygnflower> GetFlowerChannelData(int channel)
        //{
        //    return flowerLibrary[channel];
        //}

        public override void _Ready()
        {
            Dygnflower dygnflower1 = new Dygnflower();
            Dygnflower dygnflower2 = new Dygnflower();

            dygnflower1.SetData("A01-R01-F01", 1, DygnflowerStatus.Sleeping, DygnflowerType.Night, new Vector2(-9452, -16), false);
            dygnflower2.SetData("A01-R01-F02", 1, DygnflowerStatus.Sleeping, DygnflowerType.Night, new Vector2(-1180, -99), false);

            List<Dygnflower> dygnflowers1 = new List<Dygnflower> { dygnflower1, dygnflower2 };

            library.Add(1, dygnflowers1);
        }
    }
}
