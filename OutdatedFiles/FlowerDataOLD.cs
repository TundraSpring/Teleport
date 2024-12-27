using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleport.GameObjects.Enums;

namespace Teleport
{
    public class FlowerDataOLD
    {
        public Dictionary<int, Dygnflower[]> flowerLibrary = new Dictionary<int, Dygnflower[]>();

        public FlowerDataOLD()
        {
            Dygnflower dygnflower1 = new Dygnflower();
            Dygnflower dygnflower2 = new Dygnflower();

            dygnflower1.SetData("A01-R01-F01", 1, DygnflowerStatus.Sleeping, DygnflowerType.Night, new Vector2(), false);
            dygnflower2.SetData("A01-R01-F02", 1, DygnflowerStatus.Sleeping, DygnflowerType.Night, new Vector2(), false);

            Dygnflower[] dygnflowers1 = new Dygnflower[] { dygnflower1, dygnflower2 };

            flowerLibrary.Add(1, dygnflowers1);
        }

        public void UpdateFlowerData()
        {
            //flowerLibrary[channel] = dygnFlowers;

        }

        public Dygnflower[] GetFlowerData(int channel)
        {
            GD.Print(flowerLibrary[channel][0].status);
            GD.Print(flowerLibrary[channel][1].status);
            return flowerLibrary[channel];
            
        }
    }
}
