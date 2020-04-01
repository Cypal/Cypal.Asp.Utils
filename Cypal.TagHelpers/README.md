# Cypal ASP Utils: Tag-Helpers

Role based authorization is a powerful model in ASP Core to restrict who does what. This is applied at a Razor Page or Controller level. There is no simpler way of providing the similar access to individual controls. This library aims to provide Role based controlls for the individual form fields.

## Overview

The form fields are created with Razor's Tag Helpers. This library adds two features show/hide and edit/read-only features for the form fields. 

Installation
-------------

This library is available as a NuGet package. You can install it using the NuGet Package Console window:

```
PM> Install-Package Cypal.TagHelpers
```

No other initialization is required. 


Usage
-----

Show/Hide

Showing or hiding based on the Roles will be done on the div/ul/li/span elements. 


```html

<!--  Show a menu item for Admins and Developers -->
<li class="nav-item" asp-shown-for="Admin,Developer">
    <a asp-page="/AdminDashboard/Index" class="nav-link">Admin Dashboard</a>
</li>

<!--  Hide a form field for Data Entry Operators -->
<div class="form-group" asp-hidden-for="DataEntryOperator">
    <label asp-for="Model.SomeField"></label>
    <input asp-for="Model.SomeField" class="form-control">
    <span asp-validation-for="Model.SomeField" class="text-danger"></span>
</div>

```

Edit/Read-Only
------------------

Editing or Read-Only is applicable for form field elements. 

```html

<!--  Editable only for Admins -->
<div class="form-group ">
    <label asp-for="Model.SomeField"></label>
    <input asp-for="Model.SomeField" class="form-control" asp-editable-for="Admin" >
    <span asp-validation-for="Model.SomeField" class="text-danger"></span>
</div>

<!--  Read-only for Office Staff -->
<div class="form-group ">
    <label asp-for="Model.SomeField"></label>
    <input asp-for="Model.SomeField" class="form-control" asp-readonly-for="OfficeStaff" >
    <span asp-validation-for="Model.SomeField" class="text-danger"></span>
</div>

```