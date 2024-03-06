﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace RGB.Back.Models;

public partial class Message
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int BoardId { get; set; }

    public string Text { get; set; }

    public DateTime Time { get; set; }

    public virtual Board Board { get; set; }

    public virtual Member Member { get; set; }

    public virtual ICollection<Picture> Pictures { get; set; } = new List<Picture>();
}