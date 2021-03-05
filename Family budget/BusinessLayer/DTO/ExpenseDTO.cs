﻿using Family_budget.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Family_budget.BusinessLayer.DTO
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDateTime { get; set; }
        public Member Member { get; set; }
    }
}