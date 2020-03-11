using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebStore.Domain.Dto.Products;
using WebStore.Domain.Entities;

namespace WebStore.Services.Mapping
{
    public static class SectionMapper
    {
        public static SectionDto ToDTO(this Section Section) => Section is null ? null : new SectionDto
        {
            Id = Section.Id,
            Name = Section.Name,
            Order = Section.Order,
            ParentId = Section.ParentId
        };

        public static Section FromDTO(this SectionDto Section) => Section is null ? null : new Section
        {
            Id = Section.Id,
            Name = Section.Name,
            Order = Section.Order,
            ParentId = Section.ParentId
        };
        public static IEnumerable<SectionDto> ToDTO(this IEnumerable<Section> Sections) => Sections?.Select(ToDTO);

        public static IQueryable<SectionDto> TODTO(this IQueryable<Section> Sections) => Sections.Select(
            Section => new SectionDto
            {
                Id = Section.Id,
                Name = Section.Name,
                Order = Section.Order,
                ParentId = Section.ParentId
            });
    }
}
