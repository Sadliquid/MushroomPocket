using System;
using System.Collections.Generic;

namespace MushroomPocket{
    public class MushroomMaster{
        public string Name {get;set;}
        public int NoToTransform {get; set;}
        public  string TransformTo {get; set;}

        public MushroomMaster(string name, int noToTransform, string transformTo){
            this.Name = name;
            this.NoToTransform = noToTransform;
            this.TransformTo = transformTo;
        }
    }
  
  







  
}