﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using Microsoft.EntityFrameworkCore;
using OptTorgWeb.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OptTorgWebDB.Models;

public partial class Employees
{
    public long IdEmployees { get; set; }

    public long? PositionId { get; set; }

    [Required(ErrorMessage = ErrorMessages.IsRequired)]
    [RegularExpression(@"^[а-яА-Я]+$", ErrorMessage = ErrorMessages.OnlyRuLetters)]
    public string Surname { get; set; }

    [Required(ErrorMessage = ErrorMessages.IsRequired)]
    [RegularExpression(@"^[а-яА-Я]+$", ErrorMessage = ErrorMessages.OnlyRuLetters)]
    public string Name { get; set; }

    [Required(ErrorMessage = ErrorMessages.IsRequired)]
    [RegularExpression(@"^[а-яА-Я]+$", ErrorMessage = ErrorMessages.OnlyRuLetters)]
    public string Patronomic { get; set; }

    public DateOnly? BirthDate { get; set; }

    [Required(ErrorMessage = ErrorMessages.IsRequired)]
    [RegularExpression(@"^ул\. [А-Яа-яЁёA-Za-z\s\-]+, (д\.|корп\.) [А-Яа-яЁёA-Za-z0-9]+(, (д\.|корп\.) [А-Яа-яЁёA-Za-z0-9]+)?$",ErrorMessage = ErrorMessages.WrongAdressFormat)]
    public string Adress { get; set; }

    [Required(ErrorMessage = ErrorMessages.IsRequired)]
    [RegularExpression(@"^[а-яА-Я]+$", ErrorMessage = ErrorMessages.OnlyRuLetters)]
    public string City { get; set; }

    [Required(ErrorMessage = ErrorMessages.IsRequired)]
    [RegularExpression(@"^[а-яА-Я]+$", ErrorMessage = ErrorMessages.OnlyRuLetters)]
    public string Oblast { get; set; }

    [Required(ErrorMessage = ErrorMessages.IsRequired)]
    [RegularExpression(@"^\d{6}$", ErrorMessage = ErrorMessages.WrongPostIndexFormat)]
    public int PostIndex { get; set; }

    [Required(ErrorMessage = ErrorMessages.IsRequired)]
    [RegularExpression(@"^[а-яА-Я]+$", ErrorMessage = ErrorMessages.OnlyRuLetters)]
    public string State { get; set; }

    [Required(ErrorMessage = ErrorMessages.IsRequired)]
    public string Email { get; set; }

    [Required(ErrorMessage = ErrorMessages.IsRequired)]
    [RegularExpression(@"^(\+7|8)[\s\-]?\(?\d{3}\)?[\s\-]?\d{3}[\s\-]?\d{2}[\s\-]?\d{2}$",ErrorMessage = ErrorMessages.WrongPhoneFormat)]
    public string WorkPhone { get; set; }

    [Required(ErrorMessage = ErrorMessages.IsRequired)]
    [RegularExpression(@"^\d{10}(\d{2})?$",ErrorMessage = "ИНН должен содержать 10 цифр (для юрлиц) или 12 цифр (для физлиц/ИП)")]
    public int Inn { get; set; }

    [Required(ErrorMessage = ErrorMessages.IsRequired)]
    [MinLength(3, ErrorMessage = "Минимальная длинна пароля - 3 символа")]
    public string PassWord { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<Delivery> DeliveryEmployeeAccept { get; set; } = new List<Delivery>();

    public virtual ICollection<Delivery> DeliveryEmployeeReceive { get; set; } = new List<Delivery>();

    public virtual Positions Position { get; set; }

    public virtual ICollection<Sending> Sending { get; set; } = new List<Sending>();

    public virtual ICollection<StorageEmployees> StorageEmployeesNavigation { get; set; } = new List<StorageEmployees>();

    public static List<Employees> GetAllEmployees()
    {
        var db = new OptTorgDBContext();
        var p = db.Employees.Include(x =>x.Position);
        return p.Where(x => x.Active == true).ToList();
    }

    public static void CreatrEmployees(Employees p)
    {
        var db = new OptTorgDBContext();
        p.Active = true;
        db.Employees.Add(p);
        db.SaveChanges();
    }

    public static IEnumerable<Employees> GetNotAssignedStorageEmployees()
    {
        var db = new OptTorgDBContext();
        var p = db.Employees.Where(x => x.Active == true);
        
        var seempId = db.StorageEmployees.Select(x => x.EmployeeId).ToList();
        var empId = db.Employees.Select(x => x.IdEmployees).ToList();

        var notAssignedSe = empId.Except(seempId);

        return p.Where(x => notAssignedSe.Contains(x.IdEmployees)).Include(x => x.Position).ToList();
    }

    public static Employees GetEmployeesById(int id)
    {
        var db = new OptTorgDBContext();
        return db.Employees.Single(x => x.IdEmployees == id);
    }

    public static Employees GetEmployeesByIdForLogin(long id)
    {
        var db = new OptTorgDBContext();
        return db.Employees.FirstOrDefault(x => x.IdEmployees == id && x.Active);
    }

    public static void UpdateEmployees(Employees p)
    {
        var db = new OptTorgDBContext();
        p.Active = true;
        db.Employees.Update(p);
        db.SaveChanges();
    }

    public static void DeleteEmployees(int id)
    {
        var db = new OptTorgDBContext();
        var pos = db.Employees.FirstOrDefault(x => x.IdEmployees == id);
        pos.Active = false;

        db.SaveChanges();
    }
    public static void DCascade(int id)
    {
        var db = new OptTorgDBContext();
        var strg = db.Employees.FirstOrDefault(x => x.IdEmployees == id);

        db.Remove(strg);
        db.SaveChanges();
    }
}