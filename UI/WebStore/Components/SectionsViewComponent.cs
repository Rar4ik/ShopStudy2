﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
    //[ViewComponent(Name = "Sections")]
    public class SectionsViewComponent : ViewComponent
    {
        private readonly IProductData _ProductData;
        public SectionsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        //public async Task<IViewComponentResult> InvokeAsync() => View();
        public IViewComponentResult Invoke(string SectionId)
        {
            var section_id = int.TryParse(SectionId, out var id) ? id : (int?) null;
            var serctions = GetSections(section_id, out var parent_section_id);

            return View(new SectionCompleteViewModel
            {
                Sections = serctions,
                CurrentSectionId = section_id,
                CurrentParentSection = parent_section_id
            });
        }

        private IEnumerable<SectionViewModel> GetSections(int? SectionId, out int? ParentSectionId)
        {
            ParentSectionId = null;

            var sections = _ProductData.GetSections();

            var parent_sections = sections.Where(section => section.ParentId is null).ToArray();

            var parent_sections_views = parent_sections
               .Select(parent_section => new SectionViewModel
               {
                   Id = parent_section.Id,
                   Name = parent_section.Name,
                   Order = parent_section.Order
               })
               .ToList();

            foreach (var parent_section_view in parent_sections_views)
            {
                var childs = sections.Where(section => section.ParentId == parent_section_view.Id);
                foreach (var child_section in childs)
                {
                    if (child_section.Id == SectionId)
                        ParentSectionId = parent_section_view.Id;

                    parent_section_view.ChildSections.Add(
                        new SectionViewModel
                        {
                            Id = child_section.Id,
                            Name = child_section.Name,
                            Order = child_section.Order,
                            ParentSection = parent_section_view
                        });
                }
                parent_section_view.ChildSections.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            }

            parent_sections_views.Sort((a, b) => Comparer<int>.Default.Compare(a.Order, b.Order));
            return parent_sections_views;
        }
    }
}
