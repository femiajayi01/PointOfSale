using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Http;
using PosCore.Models;

namespace PosCore.ViewModels
{
    public class EditProductViewModel : CreateProductViewModel
    {
        public string Id { get; set; }

    }
}
