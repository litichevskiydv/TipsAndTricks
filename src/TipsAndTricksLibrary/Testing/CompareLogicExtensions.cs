﻿namespace TipsAndTricksLibrary.Testing
{
    using System;
    using System.Linq.Expressions;
    using KellermanSoftware.CompareNetObjects;

    public static class CompareLogicExtensions
    {
        public static CompareLogic ExcludePropertyFromComparison<TEntity, TProperty>
            (this CompareLogic compareLogic, Expression<Func<TEntity, TProperty>> ignoredMember)
        {
            var nameIgnoredField = ((MemberExpression)ignoredMember.Body).Member.Name;
            compareLogic.Config.MembersToIgnore.Add(nameIgnoredField);
            return compareLogic;
        }

        public static CompareLogic SetupMaxDifferencesCount(this CompareLogic compareLogic, int maxDifferencesCount)
        {
            compareLogic.Config.MaxDifferences = maxDifferencesCount;
            return compareLogic;
        }

        public static CompareLogic SetupCollectionsOrderChecking(this CompareLogic compareLogic, bool ignoreCollectionsOrder)
        {
            compareLogic.Config.IgnoreCollectionOrder = ignoreCollectionsOrder;
            return compareLogic;
        }
    }
}