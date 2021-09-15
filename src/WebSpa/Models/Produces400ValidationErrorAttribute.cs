using Microsoft.AspNetCore.Mvc;
using System;

namespace WebSpa.Models
{
    public class Produces400ValidationErrorAttribute : ProducesResponseTypeAttribute
    {
        public Produces400ValidationErrorAttribute() : base(400)
        {
        }
    }
}