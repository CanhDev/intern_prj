namespace intern_prj.Data_response
{
    public class ChangeOrderStatusRes
    {
       public int orderId { get; set; }
       public string StatusPayment { get; set; } = "";
       public string StatusShipping { get; set; } = "";
    }
}
