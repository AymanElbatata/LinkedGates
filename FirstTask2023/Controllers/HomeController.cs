using FirstTask2023.DAL.Models;
using FirstTask2023.DAL.Repository;
using FirstTask2023.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;

namespace FirstTask2023.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGenericRepository<DeviceTBL> Device;
        private readonly IGenericRepository<DeviceCategoryTBL> DeviceCategory;
        private readonly IGenericRepository<PropertiesTBL> Properties;
        private readonly IGenericRepository<DevicePropertyTBL> DeviceProperty;
        private readonly IGenericRepository<BrandTBL> Brand;
        private readonly IGenericRepository<UserTBL> User;
        private readonly IGenericRepository<DevicePropertyDetailTBL> DevicePropertyDetail;

        public HomeController(IGenericRepository<DeviceTBL> Device, IGenericRepository<DeviceCategoryTBL> DeviceCategory,
               IGenericRepository<PropertiesTBL> Properties, IGenericRepository<DevicePropertyTBL> DeviceProperty,
               IGenericRepository<BrandTBL> Brand, IGenericRepository<UserTBL> User,
               IGenericRepository<DevicePropertyDetailTBL> DevicePropertyDetail)
        {
            this.Device = Device;
            this.DeviceCategory = DeviceCategory;
            this.Properties = Properties;
            this.DeviceProperty = DeviceProperty;
            this.Brand = Brand;
            this.User = User;
            this.DevicePropertyDetail = DevicePropertyDetail;
        }

        public IActionResult Index()
        {
            try
            {
                ViewBag.Categories = DeviceCategory.ListAll();
                var data = Device.ListAll();

                return View(data);
            }
            catch (Exception ex)
            {
                ViewBag.Errors = ex.Message;
                return View();
            }
        }


        public IActionResult Create(int id)
        {
            try
            {
                ViewBag.Brands = Brand.ListAll();
                ViewBag.Users = User.ListAll();
                //var category = DeviceCategory.GetByIdAsync(id);
                var deviceProperty = DeviceProperty.FindByQuery(x => x.DeviceCategoryId == id).ToList();
                var request = new AddDeviceViewModel
                {

                    DeviceCategoryId = id,
                    DevicePropertyDetailViewModel = new List<DevicePropertyDetailViewModel>()
                };
                foreach (var item in deviceProperty)
                {
                    DevicePropertyDetailViewModel row = new DevicePropertyDetailViewModel();
                    row.PropertyId = Convert.ToInt32(item.PropertyId);
                    row.PropName = item.Property.Name;
                    request.DevicePropertyDetailViewModel.Add(row);
                }
                return View(request);
            }
            catch (Exception ex)
            {
                ViewBag.Errors = ex.Message;
                return View();
            }
            
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(AddDeviceViewModel model)
        {
            try
            {
                var newdevice = new DeviceTBL
                {
                    CurrentUserId = model.CurrentUserId,
                    AcquisitionDate = model.AcquisitionDate,
                    BrandId = model.BrandId,
                    Memo = model.Memo,
                    DeviceCategoryId = model.DeviceCategoryId,
                    Name = model.Name,
                    SerialNo = model.SerialNo
                };
                var SavedDevice = await Device.Add(newdevice);

                foreach (var item in model.DevicePropertyDetailViewModel)
                {
                    var newDeviceDetail = new DevicePropertyDetailTBL
                    {
                        DeviceId = Convert.ToInt32(SavedDevice.Id),
                        PropertyId = Convert.ToInt32(item.PropertyId),
                        Value = item.Value
                    };
                    DevicePropertyDetail.Add(newDeviceDetail);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Errors = ex.Message;
                return View();
            }
            
        }

        public IActionResult Update(int id)
        {
            try
            {
                ViewBag.Brands = Brand.ListAll();
                ViewBag.Users = User.ListAll();
                ViewBag.DeviceCategory = DeviceCategory.ListAll();

                var RequiredDevice = Device.GetByIdAsync(id);
                var deviceProperty = DevicePropertyDetail.FindByQuery(x => x.DeviceId == id).ToList();
                var request = new UpdateDeviceViewModel
                {

                    Id = id,
                    CurrentUserId = RequiredDevice.CurrentUserId,
                    AcquisitionDate = RequiredDevice.AcquisitionDate,
                    BrandId = RequiredDevice.BrandId,
                    Memo = RequiredDevice.Memo,
                    DeviceCategoryId = RequiredDevice.DeviceCategoryId,
                    Name = RequiredDevice.Name,
                    SerialNo = RequiredDevice.SerialNo,

                    DevicePropertyDetailViewModel = new List<DevicePropertyDetailViewModel>()
                };
                foreach (var item in deviceProperty)
                {
                    DevicePropertyDetailViewModel row = new DevicePropertyDetailViewModel();
                    row.Id = item.Id;
                    row.PropertyId = Convert.ToInt32(item.PropertyId);
                    row.PropName = item.Property.Name;
                    row.Value = item.Value;
                    request.DevicePropertyDetailViewModel.Add(row);
                }
                return View(request);
            }
            catch (Exception ex)
            {
                ViewBag.Errors = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(UpdateDeviceViewModel model)
        {
            try
            {
                var device = Device.GetByIdAsync(model.Id);
                device.SerialNo = model.SerialNo;
                device.CurrentUserId = model.CurrentUserId;
                device.AcquisitionDate = model.AcquisitionDate;
                device.BrandId = model.BrandId;
                device.Memo = model.Memo;
                device.DeviceCategoryId = model.DeviceCategoryId;
                device.Name = model.Name;
                await Device.Update(device);

                foreach (var item in model.DevicePropertyDetailViewModel)
                {
                    var EditDeviceDetail = DevicePropertyDetail.GetByIdAsync(item.Id);
                    {
                        EditDeviceDetail.Value = item.Value;
                    };
                    await DevicePropertyDetail.Update(EditDeviceDetail);
                }

                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                ViewBag.Errors = ex.Message;
                return View();
            }
        }

    }
}