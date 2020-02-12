using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;
    class Block : Sprite
    {
        public Block() : base("dirt.png")
        {
            
        }
        
        void OnCollision(GameObject other)
        {
            int timer = 0;
            if (other is Player && Input.GetKey(Key.Z))
            {
                timer++;
                if(timer>25)
                {
                    LateDestroy();
                    timer = 0;
                }
            }
        }
    }
