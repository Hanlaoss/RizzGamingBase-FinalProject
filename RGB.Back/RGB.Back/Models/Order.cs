﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace RGB.Back.Models;

public partial class Order
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int GameId { get; set; }

    public DateOnly OrderDate { get; set; }

    public virtual Game Game { get; set; }

    public virtual Member IdNavigation { get; set; }
}