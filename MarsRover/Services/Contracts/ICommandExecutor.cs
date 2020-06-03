using System;
using System.Collections.Generic;
using System.Text;
using MarsRover.Models.Enums;

namespace MarsRover.Services.Contracts
{
    public interface ICommandExecutor
    {        
        void Execute(string command);        
    }
}
