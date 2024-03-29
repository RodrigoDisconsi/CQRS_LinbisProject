﻿using CQRSLinbis.Application.Common.Models;
using CQRSLinbis.Application.Developers.Queries.Models;

namespace CQRSLinbis.Application.Developers.Queries.GetDevelopers.Response
{
    public class GetDevelopersResponse
    {
        public PaginatedList<DeveloperDto> Developers {  get; set; }
    }
}
