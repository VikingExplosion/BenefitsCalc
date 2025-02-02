﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BenefitsCalc.Data;
using BenefitsCalc.Models;

namespace BenefitsCalc.Pages.Employees {
	public class IndexModel : PageModel {
		private readonly BenefitsCalc.Data.BenefitsCalcContext _context;

		public IndexModel(BenefitsCalc.Data.BenefitsCalcContext context) {
			_context = context;
		}

		public IList<Employee> Employee { get; set; }

		//onget void or ongetasync task, no return
		public async Task OnGetAsync() {
			Employee = await _context.Employee.ToListAsync();
		}
	}
}
