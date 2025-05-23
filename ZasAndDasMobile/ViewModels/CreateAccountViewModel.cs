﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZasUndDas.Shared.Services;

namespace ZasAndDasMobile.ViewModels;

public partial class CreateAccountViewModel(IAPIService service) : ObservableValidator
{
    public Action<bool, string>? Logout { get; set; }
    [ObservableProperty]
    public partial bool LoggedOut { set; get; } = true;
    [ObservableProperty]

    public partial bool LoggedIn { set; get; } = false;
    [ObservableProperty]
    [MaxLength(50)]
    [Required]
    public partial string Name { set; get; }

    [ObservableProperty]
    [MaxLength(50)]
    [EmailAddress]
    [Required]
    public partial string Email { set; get; }


    [ObservableProperty]
    [MaxLength(50)]
    [MinLength(7)]
    [Required(ErrorMessage = "Password is required.")]
    [CompareTo(nameof(ComparePassword))]
    [SpecialCharecter]
    public partial string Password { set; get; }

    [ObservableProperty]
    [MaxLength(50)]
    [Required(ErrorMessage = "Confirm Password is required.")]
    public partial string ComparePassword { set; get; }

    public ObservableCollection<string> NameErrors => new(GetErrors(nameof(Name)).Select<ValidationResult, string>(p => p.ErrorMessage!));
    public ObservableCollection<string> EmailErrors => new(GetErrors(nameof(Email)).Select<ValidationResult, string>(p => p.ErrorMessage!));
    public ObservableCollection<string> Password1Errors => new(GetErrors(nameof(Password)).Select<ValidationResult, string>(p => p.ErrorMessage!));
    public ObservableCollection<string> Password2Errors => new(GetErrors(nameof(ComparePassword)).Select<ValidationResult, string>(p => p.ErrorMessage!));

    partial void OnLoggedOutChanged(bool value)
    {
        LoggedIn = !LoggedOut;
    }
    [RelayCommand]
    public async Task ReturnToHome()
    {
        await Shell.Current.GoToAsync("///MainPage");
    }

    [RelayCommand]
    public async Task ValidateAndSend()
    {
        ValidateAllProperties();
        OnPropertyChanged(nameof(NameErrors));
        OnPropertyChanged(nameof(EmailErrors));
        OnPropertyChanged(nameof(Password1Errors));
        OnPropertyChanged(nameof(Password2Errors));
        if (HasErrors)
        {
            return;
        }
        await service.CreateAccount(new() { Email = Email, Name = Name, PassCode = Password });
        if (Logout != null)
            Logout(false, Name);
    }
    [RelayCommand]
    public void LogOut()
    {
        service.LogOut();
        if (Logout != null)
            Logout(true, string.Empty);
        Password = string.Empty;
    }
}
public class SpecialCharecterAttribute : ValidationAttribute
{
    public SpecialCharecterAttribute()
    {

    }
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return new ValidationResult("You Need a Password");
        if (!((string)value).Any(p => (new char[]
{
    '!', '@', '#', '$', '%', '^', '&', '*', '(', ')',
    '-', '_', '=', '+', '[', ']', '{', '}', '\\', '|',
    ';', ':', '\'', '\"', ',', '<', '.', '>', '/', '?', '`', '~'
}).Contains(p)))
            return new ValidationResult("Password must contain a special character");
        if (!((string)value).Any(p => (new char[]
{
        '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'
}).Contains(p)))
            return new ValidationResult("Password must contain a number");

        return ValidationResult.Success;
    }
}
public class CompareToAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public CompareToAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);
        if (comparisonProperty == null)
        {
            return new ValidationResult($"Unknown property: {_comparisonProperty}");
        }

        var comparisonValue = comparisonProperty.GetValue(validationContext.ObjectInstance);

        if (!object.Equals(value, comparisonValue))
        {
            return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must match {_comparisonProperty}.");
        }

        return ValidationResult.Success;
    }

}
