using System;
using System.Collections.Generic;
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
            Name = Section.Name
        };

        public static Section FromDTO(this SectionDto Section) => Section is null ? null : new Section
        {
            Id = Section.Id,
            Name = Section.Name
        };
    }
}
