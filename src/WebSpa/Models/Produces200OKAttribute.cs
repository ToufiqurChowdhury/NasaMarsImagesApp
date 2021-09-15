using Microsoft.AspNetCore.Mvc;
using System;

namespace WebSpa.Models
{
    public class Produces200OKAttribute : ProducesResponseTypeAttribute
    {
        public Produces200OKAttribute() : base(200)
        {
        }
    }
}