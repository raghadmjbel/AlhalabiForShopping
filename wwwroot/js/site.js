Mr. Alhalabi, [12/19/2025 12:38 PM]
@model AlhalabiShopping.Models.AdminDashboardViewModel

@{
    ViewData["Title"] = "لوحة التحكم - مول الحلبي";
    Layout = "_Layout"; // لضمان ظهور النافبار الذهبي
}

<div class="container mt-4" dir="rtl">
    <div class="row mb-4">
        <div class="col-12 text-center">
            <h1 class="display-5 fw-bold" style="color: var(--gold);">لوحة الإدارة الذكية</h1>
            <p class="text-muted">مرحباً بك في مركز إدارة متجر الحلبي</p>
            <div class="ai-badge mb-3">نظام تحليل البيانات مدعوم بالذكاء الاصطناعي</div>
        </div>
    </div>

    <div class="row mb-5 text-center">
        <div class="col-md-3">
            <div class="card bg-dark text-white border-gold shadow">
                <div class="card-body">
                    <h6 class="text-uppercase small">إجمالي المنتجات</h6>
                    <h2 class="text-warning">@Model.TotalProducts</h2>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card bg-dark text-white border-gold shadow">
                <div class="card-body">
                    <h6 class="text-uppercase small">إجمالي الطلبات</h6>
                    <h2 class="text-warning">@Model.TotalOrders</h2>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card bg-dark text-white border-gold shadow">
                <div class="card-body">
                    <h6 class="text-uppercase small">الأرباح التقديرية</h6>
                    <h2 class="text-success">@Model.TotalRevenue.ToString("C")</h2>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card bg-dark text-white border-gold shadow">
                <div class="card-body">
                    <h6 class="text-uppercase small">المستخدمين</h6>
                    <h2 class="text-info">@Model.TotalUsers</h2>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-7 mb-4">
            <div class="card shadow-sm border-0">
                <div class="card-header bg-dark text-gold fw-bold">
                    آخر المنتجات المضافة في المول
                </div>
                <div class="card-body p-0">
                    <table class="table table-hover align-middle mb-0">
                        <thead class="table-light">
                            <tr>
                                <th>المنتج</th>
                                <th>AI Score</th>
                                <th>السعر</th>
                                <th>التحكم</th>
                            </tr>
                        </thead>
                        <tbody>
                            @if (Model.RecentProducts != null)
                            {
                                foreach (var product in Model.RecentProducts)
                                {
                                    <tr>
                                        <td class="fw-bold">@product.ProductName</td>
                                        <td>
                                            <span class="badge @(product.AIScore > 0.8 ? "bg-success" : "bg-primary")">
                                                @product.AIScore.ToString("P0")
                                            </span>
                                        </td>
                                        <td>@product.Price.ToString("C")</td>
                                        <td>
                                            <a href="/Admin/Edit/@product.ProductID" class="btn btn-sm btn-outline-dark">تعديل</a>
                                        </td>
                                    </tr>
                                }
                            }
                        </tbody>
                    </table>
                </div>
            </div>
        </div>

Mr. Alhalabi, [12/19/2025 12:38 PM]
<div class="col-lg-5">
            <div class="card shadow-sm border-0">
                <div class="card-header bg-dark text-gold fw-bold">
                    أحدث الطلبات الواصلة
                </div>
                <div class="card-body p-0">
                    <table class="table table-sm table-hover mb-0">
                        <thead class="table-light">
                            <tr>
                                <th>رقم الطلب</th>
                                <th>التاريخ</th>
                                <th>الحالة</th>
                            </tr>
                        </thead>
                        <tbody>
                            @if (Model.LatestOrders != null)
                            {
                                foreach (var order in Model.LatestOrders)
                                {
                                    <tr>
                                        <td>#@order.OrderID</td>
                                        <td class="small">@order.OrderDate.ToShortDateString()</td>
                                        <td>
                                            <span class="badge @(order.IsPaid ? "bg-success" : "bg-warning text-dark")">
                                                @(order.IsPaid ? "مدفوع" : "بانتظار الدفع")
                                            </span>
                                        </td>
                                    </tr>
                                }
                            }
                        </tbody>
                    </table>
                </div>
                <div class="card-footer text-center bg-white">
                    <a href="/Admin/ManageOrders" class="btn btn-sm btn-gold">عرض كافة الطلبات</a>
                </div>
            </div>
        </div>
    </div>
</div>

<style>
    .border-gold { border: 1px solid var(--gold) !important; }
    .text-gold { color: var(--gold); }
    .btn-gold { background-color: var(--gold); color: #000; font-weight: bold; border-radius: 5px; }
    .btn-gold:hover { background-color: #fff; border: 1px solid var(--gold); color: var(--gold); }
</style>

Mr. Alhalabi, [12/19/2025 12:41 PM]
/* --- Alhalabi Shopping Site.js --- */

// التأكد من أن الكود يعمل بعد تحميل الصفحة بالكامل
document.addEventListener("DOMContentLoaded", function () {

    console.log("Alhalabi Shopping System: Active");

    // 1. وظيفة التنبيه عند الضغط على أزرار الشراء
    const setupCartButtons = () => {
        const cartBtns = document.querySelectorAll('.btn-warning, .btn-gold');
        cartBtns.forEach(btn => {
            btn.addEventListener('click', function () {
                // استدعاء التنبيه
                showAlhalabiToast("تمت الإضافة للسلة بنجاح!");
            });
        });
    };

    // 2. وظيفة تأكيد الحذف
    const setupDeleteConfirmation = () => {
        const deleteBtns = document.querySelectorAll('.btn-danger');
        deleteBtns.forEach(btn => {
            btn.addEventListener('click', function (e) {
                if (!confirm("هل أنت متأكد من حذف هذا العنصر؟")) {
                    e.preventDefault();
                }
            });
        });
    };

    // تشغيل الوظائف
    setupCartButtons();
    setupDeleteConfirmation();
});

// وظيفة إظهار التنبيه (خارج الـ DOMContentLoaded لتكون عامة)
function showAlhalabiToast(message) {
    const toast = document.createElement('div');
    toast.style.position = 'fixed';
    toast.style.bottom = '20px';
    toast.style.right = '20px';
    toast.style.backgroundColor = '#e0b354';
    toast.style.color = '#000';
    toast.style.padding = '15px 25px';
    toast.style.borderRadius = '8px';
    toast.style.boxShadow = '0 4px 12px rgba(0,0,0,0.5)';
    toast.style.zIndex = '9999';
    toast.style.fontWeight = 'bold';
    toast.innerHTML = message;

    document.body.appendChild(toast);

    // إخفاء التنبيه بعد 3 ثوانٍ
    setTimeout(() => {
        toast.style.opacity = '0';
        toast.style.transition = 'opacity 0.5s ease';
        setTimeout(() => toast.remove(), 500);
    }, 3000);
}