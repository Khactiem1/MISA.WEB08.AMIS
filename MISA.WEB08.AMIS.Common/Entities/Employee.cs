using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MISA.WEB08.AMIS.Common.Attributes;
using MISA.WEB08.AMIS.Common.Enums;

namespace MISA.WEB08.AMIS.Common.Entities
{
    /// <summary>
    /// nhân viên ứng với bảng employee trong database
    /// </summary>
    /// Created by : Nguyễn Khắc Tiềm 21.09.2022
    public class Employee : BaseEntity
    {
        /// <summary>
        /// id nhân viên
        /// </summary>
        [PrimaryKey]
        public Guid? EmployeeID { get; set; }

        /// <summary>
        /// id đơn vị
        /// </summary>
        [IsNotNullOrEmpty("Mã đơn vị không được để trống")]
        public Guid? UnitID { get; set; }

        /// <summary>
        /// tên đơn vị
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// mã nhân viên
        /// </summary>
        [IsNotNullOrEmpty("Mã nhân viên không được để trống")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// tên nhân viên
        /// </summary>
        [IsNotNullOrEmpty("Tên nhân viên không được để trống")]
        public string EmployeeName { get; set; }

        /// <summary>
        /// ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// giới tính
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// chứng minh thư
        /// </summary>
        public string IdentityCard { get; set; }

        /// <summary>
        /// chi nhánh ngân hàng
        /// </summary>
        public string BranchBank { get; set; }

        /// <summary>
        /// chức danh
        /// </summary>
        public string EmployeeTitle { get; set; }

        /// <summary>
        /// số tài khoản
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// tên ngân hàng
        /// </summary>
        public string NameBank { get; set; }

        /// <summary>
        /// ngày cấp cmnd
        /// </summary>
        public DateTime? DayForIdentity { get; set; }

        /// <summary>
        /// địa chỉ cấp cmnd
        /// </summary>
        public string GrantAddressIdentity { get; set; }

        /// <summary>
        /// số điện thoại
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// số điện thoại cố định
        /// </summary>
        public string LandlinePhone { get; set; }

        /// <summary>
        /// điạ chỉ email
        /// </summary>
        public string EmployeeEmail { get; set; }

        /// <summary>
        /// địa chỉ nhân viên
        /// </summary>
        public string EmployeeAddress { get; set; }
    }
}