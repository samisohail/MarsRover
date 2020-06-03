using System;
using System.Collections.Generic;
using System.Text;
using MarsRover.Models.Enums;

namespace MarsRover.Services.Contracts
{
    public interface IVehicle
    {
        void Move(Movement movement);
        void Deploy(int x, int y, Cardinal cardinal);
        string ReportPosition();
    }
}
