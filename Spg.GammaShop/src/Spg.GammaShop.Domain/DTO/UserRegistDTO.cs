﻿using Spg.GammaShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Domain.DTO
{
    public class UserRegistDTO
    {
        //[MaxLength(25)]
        //[MinLength(2)]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Vorname { get; set; } = string.Empty;
        //[MaxLength(25)]
        //[MinLength(2)]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Nachname { get; set; } = string.Empty;
        public string Addrese { get; set; } = string.Empty;
        //[PhoneAttribute(ErrorMessage = "Das ist keine gültige Telefon Nummer!")]
        public string Telefon { get; set; } = string.Empty;
        ////[RegularExpression("^[A-Za-z0-9,@,.]")]
        //[EmailAddress(ErrorMessage ="Das ist keine gültige E-mail Adresse!")]
        //[RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]        
        public string Email { get; set; } = string.Empty;         //IsUnique
        public string PW { get; set; } = string.Empty;

        public UserRegistDTO(User user)
        {
            Vorname = user.Vorname;
            Nachname = user.Nachname;
            Addrese = user.Addrese;
            Telefon = user.Telefon;
            Email = user.Email;
            PW = user.PW;
        }

        public UserRegistDTO()
        {
        }
    }
    
}
