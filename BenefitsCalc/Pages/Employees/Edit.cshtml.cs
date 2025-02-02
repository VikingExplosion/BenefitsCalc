﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BenefitsCalc.Data;
using BenefitsCalc.Models;

namespace BenefitsCalc.Pages.Employees {
	public class EditModel : PageModel {
		private readonly BenefitsCalc.Data.BenefitsCalcContext _context;

		public EditModel(BenefitsCalc.Data.BenefitsCalcContext context) {
			_context = context;
		}

		[BindProperty]
		public Employee Employee { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id) {
			if (id == null) {
				return NotFound();
			}

			Employee = await _context.Employee.FirstOrDefaultAsync(m => m.ID == id);

			if (Employee == null) {
				return NotFound();
			}
			return Page();
		}

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync() {
			if (!ModelState.IsValid) {
				return Page();
			}

			//recalc in case of name change
			Employee = _context.CalcBenefits(Employee);

			_context.Attach(Employee).State = EntityState.Modified;

			try {
				await _context.SaveChangesAsync();
			} catch (DbUpdateConcurrencyException) {
				if (!EmployeeExists(Employee.ID)) {
					return NotFound();
				} else {
					throw;
				}
			}

			return RedirectToPage("../Index");
		}

		private bool EmployeeExists(int id) {
			return _context.Employee.Any(e => e.ID == id);
		}
	}
}
