# Cypal ASP Utils

## Overview

This repo contains some of the code I wrote while learning ASP Core. (If you are a developer and have not worked in .Net Core, I would recommend to give a try to both .Net Core and ASP Core). I thought it would be useful for wider set of audience beyond me, hence published it to GitHub and made it as Open Source. Feel free to use and raise a PR/issue, if you see any need for a change.

## Multi-Tenants

In a typical SaaS Project, you may want to "White Label" you product. Essentially brand your product based on the Customer. In that case, instead of branching out code for every brand you support, its better to maintain a single branch for all the code. It is easier for maintenance. Even during deployment, it is easier and cheaper, to deploy to a single App Server and serve all the brands via separate URLs. In the code, you can identify each "tenant" via the URL and brand the page accordingly (or customize a feature). This library helps you to provide multi-tenant support in an ASP Core application.
[More Details](Cypal.MultiTenants/README.md)


# Tag-Helpers

Role based authorization is a powerful model in ASP Core to restrict who does what. This is applied at a Razor Page or Controller level. There is no simpler way of providing the similar access to individual controls. This library aims to provide Role based controlls for the individual form fields.
[More Details](Cypal.TagHelpers/README.md)
