
# Store Application

This repository is a sample store application developed using ASP.NET Core 9.0 and Entity Framework (EF) Core 9.0.

## Overview

The project demonstrates how to build a modern store application using ASP.NET Core for the backend and EF Core for database operations. ASP.NET Core provides high performance and cross-platform capabilities, while EF Core allows seamless interaction with the database through .NET objects.

## Features

- Post management: Create, edit, and delete store posts.
- User authentication and authorization.
- Responsive design for web accessibility.
- Open-source and extendable for learning and contributions.

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- SQL Server or any supported database
- Visual Studio or any other IDE for .NET development

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/fhazraty/WebStore.git
   ```
2. Navigate to the project directory:
   ```bash
   cd WebStore
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Update database migrations:
   ```bash
   dotnet ef database update
   ```
5. Run the application:
   ```bash
   dotnet run
   ```

## Strengths

- Leverages modern frameworks for performance, security, and scalability.
- Provides a clear example for developers to understand ASP.NET Core and EF Core usage.
- Open for community contributions and improvements.

## Recommendations

- Add more detailed documentation about code and implementation.
- Include a feature list in the documentation.
- Add screenshots or demo links for better visualization of the application.

## Contribution

Contributions are welcome! Please fork the repository and submit a pull request with your improvements.

---

# برنامه فروشگاه

این مخزن یک برنامه فروشگاه نمونه است که با استفاده از ASP.NET Core 9.0 و Entity Framework (EF) Core 9.0 توسعه یافته است.

## معرفی

پروژه نشان می‌دهد که چگونه می‌توان یک برنامه فروشگاه مدرن را با استفاده از ASP.NET Core برای بک‌اند و EF Core برای عملیات پایگاه‌داده ساخت. ASP.NET Core قابلیت‌های چندسکویی و عملکرد بالا را فراهم می‌کند، در حالی که EF Core ارتباط با پایگاه‌داده را از طریق اشیاء .NET آسان می‌کند.

هدف از ایجاد این پروژه آموزش الگوی Repository در کلاس های مجتمع فنی تهران شعبه استان البرز - آزادگان می باشد.



## ویژگی‌ها

- مدیریت پست‌ها: ایجاد، ویرایش و حذف پست‌ها.
- احراز هویت و مجوزدهی کاربران.
- طراحی واکنش‌گرا برای دسترسی‌پذیری وب.
- متن‌باز و قابل توسعه برای یادگیری و مشارکت.

## شروع کار

### پیش‌نیازها

- .NET 9.0 SDK
- SQL Server یا هر پایگاه‌داده پشتیبانی‌شده
- Visual Studio یا هر IDE دیگر برای توسعه .NET
(پیشنهاد می گردد برای ساده سازی فرآیند راه اندازی از Visual Studio 2022 استفاده نمایید.)
### نصب

1. مخزن را کلون کنید:
   ```bash
   git clone https://github.com/fhazraty/WebStore.git
   ```
2. به دایرکتوری پروژه بروید:
   ```bash
   cd WebStore
   ```
3. وابستگی‌ها را بازیابی کنید:
   ```bash
   dotnet restore
   ```
4. مهاجرت‌های پایگاه‌داده را به‌روزرسانی کنید. دقت کنید که Connectionstring در فایل CMSContext.cs باید به پایگاه داده شما اشاره کند. 
   ```bash
   dotnet ef database update
   ```
5. برنامه را اجرا کنید:
   ```bash
   dotnet run
   ```

## نقاط قوت

- استفاده از فریمورک‌های مدرن برای عملکرد، امنیت و مقیاس‌پذیری بالا.
- ارائه یک نمونه واضح برای توسعه‌دهندگان جهت درک استفاده از ASP.NET Core و EF Core.
- باز بودن برای مشارکت‌ها و بهبودهای جامعه.

## توصیه‌ها

- افزودن مستندات دقیق‌تر در مورد کد و پیاده‌سازی.
- درج فهرست ویژگی‌ها در مستندات.
- افزودن تصاویر یا لینک‌های دمو برای نمایش بهتر برنامه.
