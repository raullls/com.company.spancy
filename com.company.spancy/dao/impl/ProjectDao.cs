using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.company.spancy.entity;
using Common.Logging;
using NHibernate;
using Spring.Context;
using Spring.Context.Attributes;
using Spring.Stereotype;
using Spring.Transaction.Interceptor;

namespace com.company.spancy.dao.impl
{
    public class ProjectDao : BaseDao<Project>, IProjectDao
    {

    }
}