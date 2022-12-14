using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    public enum CardType
    {
        Move,
        ShockWave,
        Slash,
        Lazer

    }
    public interface ICard
    {
        CardType Type { get; }

        //Player Player { get; }
    }

