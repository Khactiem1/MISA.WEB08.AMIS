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
        /// </summary>7
        [IsNotNullOrEmpty("Mã đơn vị không được để trống.")]
        public Guid? UnitID { get; set; }

        /// <summary>
        /// mã nhân viên
        /// </summary>
        [Unique("Mã nhân viên <{0}> đã tồn tại trong hệ thống.")]
        [IsNotNullOrEmpty("Mã nhân viên không được để trống.")]
        [ColumnName("Mã nhân viên", 16, false, false)]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// tên nhân viên
        /// </summary>
        [IsNotNullOrEmpty("Tên nhân viên không được để trống.")]
        [ColumnName("Tên nhân viên", 40, false, false)]
        public string EmployeeName { get; set; }

        /// <summary>
        /// tên đơn vị
        /// </summary>
        [ColumnName("Tên đơn vị", 50, false, false)]
        public string UnitName { get; set; }

        /// <summary>
        /// ngày sinh
        /// </summary>
        [ColumnName("Ngày sinh", 20, true, false)]
        [MaxDateNow("Ngày sinh phải nhỏ hơn ngày hiện tại.")]
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// giới tính
        /// </summary>
        [ColumnName("Giới tính", 10, false, true)]
        public Gender Gender { get; set; }

        /// <summary>
        /// chứng minh thư
        /// </summary>
        [ColumnName("Chứng minh thư", 20, false, false)]
        public string IdentityCard { get; set; }

        /// <summary>
        /// chi nhánh ngân hàng
        /// </summary>
        [ColumnName("Chi nhánh ngân hàng", 40, false, false)]
        public string BranchBank { get; set; }

        /// <summary>
        /// chức danh
        /// </summary>
        [ColumnName("Chức danh", 20, false, false)]
        public string EmployeeTitle { get; set; }

        /// <summary>
        /// số tài khoản
        /// </summary>
        [ColumnName("Số tài khoản", 20, false, false)]
        public string BankAccount { get; set; }

        /// <summary>
        /// tên ngân hàng
        /// </summary>
        [ColumnName("Tên ngân hàng", 40, false, false)]
        public string NameBank { get; set; }

        /// <summary>
        /// ngày cấp cmnd
        /// </summary>
        [ColumnName("Ngày cấp chứng minh thư", 30, true, false)]
        [MaxDateNow("Ngày cấp phải nhỏ hơn ngày hiện tại.")]
        public DateTime? DayForIdentity { get; set; }

        /// <summary>
        /// địa chỉ cấp cmnd
        /// </summary>
        [ColumnName("Nơi cấp", 40, false, false)]
        public string GrantAddressIdentity { get; set; }

        /// <summary>
        /// số điện thoại
        /// </summary>
        [ColumnName("Điện thoại", 20, false, false)]
        [PhoneNumber("Không đúng định dạng số điện thoại.")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// số điện thoại cố định
        /// </summary>
        [ColumnName("Điện thoại cố định", 20, false, false)]
        [PhoneNumber("Không đúng định dạng số điện thoại cố định.")]
        public string LandlinePhone { get; set; }

        /// <summary>
        /// điạ chỉ email
        /// </summary>
        [ColumnName("Địa chỉ Email", 30, false, false)]
        [Email("Không đúng định dạng email.")]
        public string EmployeeEmail { get; set; }

        /// <summary>
        /// địa chỉ nhân viên
        /// </summary>
        [ColumnName("Địa chỉ", 40, false, false)]
        public string EmployeeAddress { get; set; }
    }
}