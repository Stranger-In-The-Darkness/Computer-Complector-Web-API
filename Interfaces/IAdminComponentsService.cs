using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ComputerComplectorWebAPI.Models;

namespace ComputerComplectorWebAPI.Interfaces
{
    public interface IAdminComponentsService
    {
        #region Add
        Body AddBody(Body element);

        Charger AddCharger(Charger element);

        Cooler AddCooler(Cooler element);

        CPU AddCPU(CPU element);

        HDD AddHDD(HDD element);

        Motherboard AddMotherboard(Motherboard element);

        RAM AddRAM(RAM element);

        SSD AddSSD(SSD element);

        Videocard AddVideocard(Videocard element);
        #endregion

        #region Remove
        bool RemoveBody(int id);
        bool RemoveBody(Body element);
             
        bool RemoveCharger(int id);
        bool RemoveCharger(Charger element);
             
        bool RemoveCooler(int id);
        bool RemoveCooler(Cooler element);
             
        bool RemoveCPU(int id);
        bool RemoveCPU(CPU element);
             
        bool RemoveHDD(int id);
        bool RemoveHDD(HDD element);
             
        bool RemoveMotherboard(int id);
        bool RemoveMotherboard(Motherboard element);
             
        bool RemoveRAM(int id);
        bool RemoveRAM(RAM element);
             
        bool RemoveSSD(int id);
        bool RemoveSSD(SSD element);
             
        bool RemoveVideocard(int id);
        bool RemoveVideocard(Videocard element);
        #endregion

        #region Change
        Body ChangeBody(Body old, Body @new);
             
        Charger ChangeCharger(Charger old, Charger @new);
             
        Cooler ChangeCooler(Cooler old, Cooler @new);
             
        CPU ChangeCPU(CPU old, CPU @new);
             
        HDD ChangeHDD(HDD old, HDD @new);
             
        Motherboard ChangeMotherboard(Motherboard old, Motherboard @new);
             
        Random ChangeRAM(RAM old, RAM @new);
             
        SSD ChangeSSD(SSD old, SSD @new);
             
        Videocard ChangeVideocard(Videocard old, Videocard @new);
        #endregion

        #region Property
        bool AddProperty(string component, string property);

        bool RemoveProperty(string component, string property);

        bool ChangeProperty(string component, string oldValue, string newValue);
        #endregion

        #region Field
        bool AddField(string propertyKey, string field);

        bool RemoveField(string propertyKey, string field);

        bool ChangeField(string propertyKey, string oldValue, string newValue);
        #endregion
    }
}
