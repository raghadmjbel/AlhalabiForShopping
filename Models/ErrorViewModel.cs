namespace AlhalabiShopping.Models
{
    public class ErrorViewModel
    {
        // هذا المعرف يساعدنا في معرفة رقم الخطأ لتتبعه في السيرفر
        public string? RequestId { get; set; }

        // خاصية برمجية لنتأكد إذا كان هناك رقم طلب للخطأ أم لا
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}