using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ComputerComplectorWebAPI.Models;

namespace ComputerComplectorWebAPI.Interfaces
{
    public interface IAdminComponentsServiceAsync
    {
        #region Add
        Task<Body> AddBody(Body element);

        Task<Charger> AddCharger(Charger element);

        Task<Cooler> AddCooler(Cooler element);

        Task<CPU> AddCPU(CPU element);

        Task<HDD> AddHDD(HDD element);

        Task<Motherboard> AddMotherboard(Motherboard element);

        Task<RAM> AddRAM(RAM element);

        Task<SSD> AddSSD(SSD element);

        Task<Videocard> AddVideocard(Videocard element);
        #endregion

        #region Remove
        Task<bool> RemoveBody(int id);
        Task<bool> RemoveBody(Body element);

        Task<bool> RemoveCharger(int id);
        Task<bool> RemoveCharger(Charger element);

        Task<bool> RemoveCooler(int id);
        Task<bool> RemoveCooler(Cooler element);

        Task<bool> RemoveCPU(int id);
        Task<bool> RemoveCPU(CPU element);

        Task<bool> RemoveHDD(int id);
        Task<bool> RemoveHDD(HDD element);

        Task<bool> RemoveMotherboard(int id);
        Task<bool> RemoveMotherboard(Motherboard element);

        Task<bool> RemoveRAM(int id);
        Task<bool> RemoveRAM(RAM element);

        Task<bool> RemoveSSD(int id);
        Task<bool> RemoveSSD(SSD element);

        Task<bool> RemoveVideocard(int id);
        Task<bool> RemoveVideocard(Videocard element);
        #endregion

        #region Change
        Task<Body> ChangeBody(Body old, Body @new);

        Task<Charger> ChangeCharger(Charger old, Charger @new);

        Task<Cooler> ChangeCooler(Cooler old, Cooler @new);

        Task<CPU> ChangeCPU(CPU old, CPU @new);

        Task<HDD> ChangeHDD(HDD old, HDD @new);

        Task<Motherboard> ChangeMotherboard(Motherboard old, Motherboard @new);

        Task<RAM> ChangeRAM(RAM old, RAM @new);

        Task<SSD> ChangeSSD(SSD old, SSD @new);

        Task<Videocard> ChangeVideocard(Videocard old, Videocard @new);
        #endregion

        #region Property
        Task<bool> AddProperty(string component, string property);

        Task<bool> RemoveProperty(string component, string property);

        Task<bool> ChangeProperty(string component, string oldValue, string newValue);
        #endregion

        #region Field
        Task<bool> AddField(string propertyKey, string field);

        Task<bool> RemoveField(string propertyKey, string field);

        Task<bool> ChangeField(string propertyKey, string oldValue, string newValue);
        #endregion
    }
}
