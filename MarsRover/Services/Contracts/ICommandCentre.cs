using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Services.Contracts
{
    public interface ICommandCentre
    {
        void Dispatch(string command);
    }
}
