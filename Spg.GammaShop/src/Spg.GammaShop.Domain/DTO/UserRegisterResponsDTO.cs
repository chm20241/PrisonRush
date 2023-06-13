﻿using Spg.GammaShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.GammaShop.Domain.DTO
{
    public class UserRegisterResponsDTO
    {

        public string Vorname { get; set; } = string.Empty;
        public string Nachname { get; set; } = string.Empty;
        public string Addrese { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;         //IsUnique

        public UserRegisterResponsDTO(User user)
        {
            Vorname = user.Vorname;
            Nachname = user.Nachname;
            Addrese = user.Addrese;
            Telefon = user.Telefon;
            Email = user.Email;
        }

        public UserRegisterResponsDTO()
        {
        }
    }
}
