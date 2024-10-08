﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BLL.DAL;

public partial class ProductStore
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int StoreId { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductStores")]
    public virtual Product Product { get; set; }

    [ForeignKey("StoreId")]
    [InverseProperty("ProductStores")]
    public virtual Store Store { get; set; }
}