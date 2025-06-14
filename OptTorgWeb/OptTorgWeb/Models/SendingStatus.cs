﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using OptTorgWeb.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OptTorgWebDB.Models;

public partial class SendingStatus
{
    [Required(ErrorMessage = ErrorMessages.IsRequired)]
    public long IdSs { get; set; }

    [Required(ErrorMessage = ErrorMessages.IsRequired)]
    [RegularExpression(@"^[а-яА-Я]+$", ErrorMessage = ErrorMessages.OnlyRuLetters)]
    public string Status { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<Sending> Sending { get; set; } = new List<Sending>();


    public static List<SendingStatus> GetAllSendingStatus()
    {
        var db = new OptTorgDBContext();
        return db.SendingStatus.Where(x => x.Active == true).ToList();
    }
    public static void CreatrSendingStatus(SendingStatus p)
    {
        var db = new OptTorgDBContext();
        p.Active = true;
        db.SendingStatus.Add(p);
        db.SaveChanges();
    }
    public static SendingStatus GetSendingStatusById(int id)
    {
        var db = new OptTorgDBContext();
        return db.SendingStatus.Single(x => x.IdSs == id);
    }
    public static void UpdateSendingStatus(SendingStatus p)
    {
        var db = new OptTorgDBContext();
        p.Active = true;
        db.SendingStatus.Update(p);
        db.SaveChanges();
    }
    public static void DeleteSendingStatus(int id)
    {
        var db = new OptTorgDBContext();
        var pos = db.SendingStatus.FirstOrDefault(x => x.IdSs == id);
        pos.Active = false;

        db.SaveChanges();
    }
    public static void DCascade(int id)
    {
        var db = new OptTorgDBContext();
        var strg = db.SendingStatus.FirstOrDefault(x => x.IdSs == id);

        db.Remove(strg);
        db.SaveChanges();
    }
}