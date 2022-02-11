/*
    Kiwi's Co-Op Mod for Half-Life: Alyx
    Copyright (c) 2022 KiwifruitDev
    All rights reserved.
    This software is licensed under the MIT License.
    -----------------------------------------------------------------------------
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
    -----------------------------------------------------------------------------
*/
using KiwisCoOpModCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlyxGamemode
{
    public class AlyxCustomizationOption_Headset : BaseCustomizationOption
    {
        public AlyxCustomizationOption_Headset() : base()
        {
            Author = "Valve";
            Name = "Headset";
            Description = "Russell? Can you hear me?";
            ModelName = "models/props/choreo_office/headset_prop.vmdl";
            Type = CustomizationOptionType.Head;
            DisplayImageBase64 = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAB8ElEQVRIDc2TUU/aUBTH/WhkIwEfTHC6BedCZ0AlmY2PRsE+W2kXGS1CERLcNEFoISTFpFQCBT7Xf7kP3LWV25QQkj00Pb33nN/vntt7t+bzOTb5bG0STtj/l8A0TSilX+h2DTSbTVSrFQyHw8AtDt1Bv9/H4dcDTCYTCiSxoih4eKjRMf+WhxbwPA9VVZaC2u02RPFm6VwowXQ6xXeOw/Pz01IIWXWr1ULh9vbdfCiBbdu4zueQu7yAUrp7B1lsiyQV0Ol0PPOBAsuy0Gg0wKVS+HHCYX9vB9nsiQewgJP3bDZDPp/zzDMFuq6D58+QyWRwfpbF5087iH78gNzVpQfgFpC4XC6j1+vRHKZAliV82d/DEfcNmfQRtrfjyJ4eg/wPP9T9PR6PIcsyzWEKCOj3YxOCcI1EIoFIJAJBEGihG+qPJUmieUwBKareq0gmk4jH49jdTUBVVVroh7q/i8UizQsU1LQqYrEYotEo0uk0HMehhW6gPyaXbzEWKGi/vKBer0MURYxGI1q0KGa9NU2juUwBOXKG7j3TLKB7vKZpKN79uytMwevAxJtt05W4Iax46jioVO49J40p6BrGSnCWlCkwDH2zgqFlYTAw15YwOyAt18o/Qx/NlbeIFHRaT/jz2Firi8AOWKtaZXzjgr961TVa/3loWQAAAABJRU5ErkJggg==";
            Default = true;
        }
    }
    public class AlyxCustomizationOption_AlyxLeftHand : BaseCustomizationOption
    {
        public AlyxCustomizationOption_AlyxLeftHand() : base()
        {
            Author = "Valve";
            Name = "Alyx's Left Hand";
            Description = "Where's my gravity gloves?";
            ModelName = "models/hands/alyx_glove_left.vmdl";
            Type = CustomizationOptionType.LeftHand;
            DisplayImageBase64 = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAC+ElEQVRIDe2UbVMbVRiG+VNVStLdbPY1m4QkkOwSpMFIC7aj2EnKaKbSAk1SqsESZ7BCEEpawRYwQGugWIEm8rsuZ1fbwU4iqNNv/fDMnnN2z33tue95Tsfx8TFvszreprij/Q5wan6nWvRse4tyeZapqUkymQyXLl9idPRTKosVms3mfwMcvviV/Z0dNr+/x6NCjot9FrFYFNu26LNtQqEgiiLTZyVYXlqi0Wi0BbU8wXz+JvO3brA4Ocbs9RHssIEoXMDvEzADOrFImIAmo/pFFEkgaVvs7e21hLQE/LL1M/dyWaZHR/iuOMHct2UqCwsk7TipD5IkeiKYuoouS2iyhKkrFAv5swPe7I293V3uz83Rbyew4zEiwQCGKqNIIuGggaZIpFIXzwZoNhoc7O9SW13hi7EsqYEkfVYvVm8M24q7WXSHw2iqgizLSJKEX/IRCpqtAb83m6wvV1gp3aZamuRhaZyVYo6liTEG4lEikW63nGCDoW503UTTDQKGgeICfKiKQigYaA2o72xTzn1OYWSAu6NpStkhZj5L8/XVD4mbGj6fiOQTEUUBWVaQVR1FUdE0DVEU3fcOyLYSrQGO3y+PjnhW26S29iNbT9bYXH3EevUB5W9muDIyjCY7FgQwTRNV05H8f1rj8XThVGfn+1QqC+0Bb4bqzDc3NhhKDxIK6ET/ssk0dBfg9Qp4PR7eO3eOWDTK2tpqS3FHp6Px8ogf7uapzuRZX5qnVl2g9uA+y6UphvoT2Im4C1A1FU/XeTfUzvNdiILA9J07/9hkLuDw4IDFr6YpZYaZG7/G4u3rzE9mqExkKXySxgxoyH4J09QxDB2vx0tPby/1er3tX5905HWjbTxcYfzyAF8OJcile7j1UYLxwQTxoEbA0NA1DUG4QLFYPJPwK8hrgLNw+NsLZm9kuTmcZOLjfgpXUuSvDqLLPizLolqt/itx16JXpJNPx7b602221x9T+2mV/eet75mTe9qN/3aCdh/9n/V3gFND/wOIA6EjE0GckgAAAABJRU5ErkJggg==";
            Default = true;
        }
    }
    public class AlyxCustomizationOption_AlyxRightHand : BaseCustomizationOption
    {
        public AlyxCustomizationOption_AlyxRightHand() : base()
        {
            Author = "Valve";
            Name = "Alyx's Right Hand";
            Description = "Where's my gravity gloves?";
            ModelName = "models/hands/alyx_glove_right.vmdl";
            Type = CustomizationOptionType.RightHand;
            DisplayImageBase64 = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAACi0lEQVRIDe3U+y9bYRzHcX/SMqv2XFRbrR7lYGq6GYq4FJMNjfuGNno5I8Fklg1FI8surJLNtVtd/673crpIJtRl+G0/fPMkJzmf1/l+n+c8WcfHx9xlZd1luJ79H7h0//5pRMlkkkQiweHh4e0D8Xgcj6eSkpIS3G43mqaxv7+fEbpyB6lUCi0SwldfS1lZKVarFVmWEAQTFW53RuRSYG9vDy0awfu0imhPByuRQfz1HkwmY7pEUcBgMLC2tnZuFxcCq6tfaWyoQ3UpNFY/YTES4MOwn8G6CnIlEVkUEEUTOYYHbGxsXA/4uBKn+vEjihQHqsvJQFMt0ec+Ag1VPMzPRTAakEw5mCWBokLnueEZf7TU7jaxYB/hjnrUQgd2mwWLZEI1iyh5MrmiQL7FTLHLma6W5qarA0dHRyzPThOP9hNqr6U4P49cSSD7/r30apZFCp0Oil0KLsVBoeJgfX396sDIYD8NVRXEQr2MNlVhFk3YLGaMhmwseTKlahEV5aWoRUp6NPPzcxnDz4wotbPF4lgPwVYvs0OdBJprsMgCsmhEEowUOGw4Hfm4lAK6u16wuXn+xv59gZ46RcnN7yyGewn7atBe+Bjv9KHazFjNEgX2P+Gtvha2t7cu/OqMwK/dLeaC3YR8NUx0PeO1vx3VbkmfIqfdysjI8JWDT5BTHeh3y7tRP9E2L2MtNWj+DkYHevCorvTsDw4ObgboamwixMxAF0PVbt5HgixOabzyVjL/Zura4XreqQ70B/upFG+Dw7z0VjLWVk9sepy+6nK+LC/dDqAjPxLfWJgcZ6y9kdlIkMG6SmKTE7cH6Ihen5YW+Lm7w0w4wOelhdsHTqCbrGf24CZh5737G6TszUE1PluHAAAAAElFTkSuQmCC";
            Default = true;
        }
    }
    public class AlyxCustomizationOption_NoHat : BaseCustomizationOption
    {
        public AlyxCustomizationOption_NoHat() : base()
        {
            Author = "KiwifruitDev";
            Name = "No Hat";
            Description = "Who needs headwear?";
            ModelName = "models/kiwimp_alyx/alyx.vmdl";
            Type = CustomizationOptionType.Hat;
            DisplayImageBase64 = "iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAM0lEQVRIDWM4evTof1piBloaDjJ71AKC8TcaRKNBRHkuH01Fo6loNBWNVjhENBiGflEBADqIb+p2Xi4kAAAAAElFTkSuQmCC";
            Default = true;
        }
    }
    public class AlyxCustomizationOption_NoCollider : BaseCustomizationOption
    {
        public AlyxCustomizationOption_NoCollider() : base()
        {
            Author = "KiwifruitDev";
            Name = "No Collider";
            Description = "I'm the invisible man!";
            ModelName = "models/kiwimp_alyx/alyx.vmdl";
            Type = CustomizationOptionType.Collider;
            DisplayImageBase64 = "iVBORw0KGgoAAAANSUhEUgAAADAAAABICAYAAACwc3YrAAAAk0lEQVRoBe3UsQ0AIRAEMfrv6+oCUcMECMnB5/yctWtm9s/f+vnx9+1+4PUFXcAF4goihBBCsQBCMaAVQgihWAChGNAKIYRQLIBQDGiFEEIoFkAoBrRCCCEUCyAUA1ohhBCKBRCKAa0QQgjFAgjFgFYIIYRiAYRiQCuEEEKxAEIxoBVCCKFYAKEY0AohhFAs8JrQAaohn5V6h7itAAAAAElFTkSuQmCC";
            Default = true;
        }
    }
}
