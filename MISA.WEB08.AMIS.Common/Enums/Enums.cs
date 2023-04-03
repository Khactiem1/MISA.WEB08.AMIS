
namespace MISA.WEB08.AMIS.Common.Enums
{
    /// <summary>
    /// Giới tính 
    /// Enum giới tính của thực thể người, 3 loại nam, nữ, khác
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public enum Gender:int
    {
        /// <summary>
        /// nam
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        Male = 0,

        /// <summary>
        /// nữ
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        Female = 1,

        /// <summary>
        /// khác
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        Other = 2,
    }

    /// <summary>
    /// Loại hàng hoá
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public enum Nature : int
    {
        /// <summary>
        /// Hàng hoá
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        Goods = 1,

        /// <summary>
        /// Dịch vụ
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        Service = 2,

        /// <summary>
        /// Nguyên vật liệu
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        Materials = 3,

        /// <summary>
        /// Thành phẩm
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        FinishedProduct = 4,

        /// <summary>
        /// Dụng cụ công cụ
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        ToolTools = 5,
    }
    /// <summary>
    /// Kiểu giảm thuế
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public enum DepreciatedTax : int
    {
        /// <summary>
        /// Không xác định
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        undefined = 1,

        /// <summary>
        /// Không giảm thuế
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        no_tax_reduction = 2,

        /// <summary>
        /// Giảm thuế
        /// Created by : Khắc Tiềm 21.09.2022
        /// </summary>
        tax_reduction = 3,
    }


    /// <summary>
    /// Kiểu join bảng
    /// </summary>
    /// Create by: Nguyễn Khắc Tiềm (21/09/2022)
    public enum TypeJoin: int
    {
        /// <summary>
        /// Kiểu inner join
        /// </summary>
        InnerJoin = 1,

        /// <summary>
        /// Kiểu left join
        /// </summary>
        LeftJoin = 2,
    }
}
