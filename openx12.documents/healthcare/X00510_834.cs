using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using openx12.Models;
using openx12.Models.Transaction;

namespace openx12.documents.healthcare
{
    public class X00510_834
    {
        public object Graph(TreeNode<Segment> tree) {
            foreach(var node in tree) {
                // walk tree, map node segments to objects
            }

            return new object();
        }

        public TreeNode<Segment> Map(Transaction tx) {
            var currentLoop = "";
            TreeNode<Segment> tree = new TreeNode<Segment>(new Segment("ROOT"));
            // TODO: add back loop indexing to allow loop grouping within a context
            // this will allow graphing to understand when a context has been re-entered
            // in order to create a new context instace
            foreach(var segment in tx.Segments) {
                segment.Index = new Models.DataElementIndex.DataIndex();
                #region Header;
                if( segment.Name.Equals("BGN")) {
                    currentLoop = HeaderLoops.Header;
                    segment.Index.Loop = currentLoop;
                    tree.AddChild(segment);
                    continue;
                }

                if( segment.Name.Equals("N1") && segment.Qualifier.Equals("P5")) {
                    currentLoop = HeaderLoops.SponsorName;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, HeaderLoops.Header);
                    continue;
                }

                if( segment.Name.Equals("N1") && segment.Qualifier.Equals("IN")) {
                    currentLoop = HeaderLoops.Payer;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, HeaderLoops.Header);
                    continue;
                }

                if( segment.Name.Equals("N1") && 
                    (segment.Qualifier.Equals("BO") || segment.Qualifier.Equals("TV"))
                ) {
                    currentLoop = HeaderLoops.TPABrokerName;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, HeaderLoops.Header);
                    continue;
                }

                if( segment.Name.Equals("ACT")) {
                    currentLoop = HeaderLoops.TPABrokerName;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, HeaderLoops.TPABrokerName);
                    continue;
                }
                #endregion

                #region Detail 
                if( segment.Name.Equals("INS")) {
                    currentLoop = HeaderLoops.MemberLevelDetail;
                    segment.Index.Loop = currentLoop;
                    tree.AddChild(segment);
                    continue;
                }

                if( segment.Name.Equals("NM1") && (
                    segment.Qualifier.Equals("74") ||
                    segment.Qualifier.Equals("IL"))
                ) {
                    currentLoop = DetailLoops.MemberName;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, HeaderLoops.MemberLevelDetail);
                    continue;
                }

                if( segment.Name.Equals("NM1") && segment.Qualifier.Equals("70")) {
                    currentLoop = DetailLoops.IncorrectMemberName;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, HeaderLoops.MemberLevelDetail);
                    continue;
                }

                if( segment.Name.Equals("NM1") && segment.Qualifier.Equals("31")) {
                    currentLoop = DetailLoops.MemberMailingAddress;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, HeaderLoops.MemberLevelDetail);
                    continue;
                }

                if( segment.Name.Equals("NM1") && segment.Qualifier.Equals("36")) {
                    currentLoop = DetailLoops.MemberEmployer;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, HeaderLoops.MemberLevelDetail);
                    continue;
                }

                if( segment.Name.Equals("NM1") && segment.Qualifier.Equals("M8")) {
                    currentLoop = DetailLoops.MemberSchool;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, HeaderLoops.MemberLevelDetail);
                    continue;
                }

                if( segment.Name.Equals("NM1") && segment.Qualifier.Equals("S3")) {
                    currentLoop = DetailLoops.CustodialParent;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, HeaderLoops.MemberLevelDetail);
                    continue;
                }

                if( segment.Name.Equals("NM1") && (
                    segment.Qualifier.Equals("6Y") ||
                    segment.Qualifier.Equals("9K") ||
                    segment.Qualifier.Equals("E1") ||
                    segment.Qualifier.Equals("EI") ||
                    segment.Qualifier.Equals("EXS") ||
                    segment.Qualifier.Equals("GB") ||
                    segment.Qualifier.Equals("GD") ||
                    segment.Qualifier.Equals("J6") ||
                    segment.Qualifier.Equals("LR") ||
                    segment.Qualifier.Equals("QD") ||
                    segment.Qualifier.Equals("S1") ||
                    segment.Qualifier.Equals("TZ") ||
                    segment.Qualifier.Equals("X4"))
                ) {
                    currentLoop = DetailLoops.ResponsiblePerson;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, HeaderLoops.MemberLevelDetail);
                    continue;
                }

                if( segment.Name.Equals("NM1") && segment.Qualifier.Equals("45")) {
                    currentLoop = DetailLoops.DropOffLocation;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, HeaderLoops.MemberLevelDetail);
                    continue;
                }

                if( segment.Name.Equals("DSB")) {
                    currentLoop = DetailLoops.DisabilityInformation;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, HeaderLoops.MemberLevelDetail);
                    continue;
                }

                if( segment.Name.Equals("HD")) {
                    currentLoop = DetailLoops.HealthCoverage;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, HeaderLoops.MemberLevelDetail);
                    continue;
                }

                if( segment.Name.Equals("LX") && currentLoop == DetailLoops.HealthCoverage) {
                    currentLoop = DetailLoops.ProviderInformation;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, DetailLoops.HealthCoverage);
                    continue;
                }

                if( segment.Name.Equals("COB")) {
                    currentLoop = DetailLoops.CoordinationOfBenefits;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, DetailLoops.HealthCoverage);
                    continue;
                }

                if( segment.Name.Equals("NM1") && currentLoop.Equals(DetailLoops.CoordinationOfBenefits)) {
                    currentLoop = DetailLoops.CoordinationOfBenefitsRelatedEntity;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, DetailLoops.CoordinationOfBenefits);
                    continue;
                }

                if( segment.Name.Equals("LS")) {
                    currentLoop = DetailLoops.AdditionalReportingCategories;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, HeaderLoops.MemberLevelDetail);
                    continue;
                }

                if( segment.Name.Equals("LX") && currentLoop.Equals(DetailLoops.AdditionalReportingCategories)) {
                    currentLoop = DetailLoops.MemberReportingCategories;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, DetailLoops.AdditionalReportingCategories);
                    continue;
                }

                if( segment.Name.Equals("LE")) {
                    currentLoop = DetailLoops.AdditionalReportingCategories;
                    segment.Index.Loop = currentLoop;
                    tree.ParentToLoop(segment, HeaderLoops.MemberLevelDetail);
                    continue;
                }
                #endregion
        
                segment.Index.Loop = currentLoop;
                tree.AppendToLoop(segment, currentLoop);
                continue;
            }
            return tree;
        }
        

        private class HeaderLoops {
            public const string Header = "1000";
            public const string SponsorName = "1000A";
            public const string Payer = "1000B";
            public const string TPABrokerName = "1000C";
            public const string TPABrokerAccountInformation = "1100C";
            public const string MemberLevelDetail = "2000";
        }

        private class DetailLoops {
            

            public const string MemberName = "2100A";
            public const string IncorrectMemberName = "2100B";
            public const string MemberMailingAddress = "2100C";
            public const string MemberEmployer = "2100D";
            public const string MemberSchool = "2100E";
            public const string CustodialParent = "2100F";
            public const string ResponsiblePerson = "2100G";
            public const string DropOffLocation = "2100H";
            public const string DisabilityInformation = "2200";
            public const string HealthCoverage = "2300";
            public const string ProviderInformation = "2310";
            public const string CoordinationOfBenefits = "2320";
            public const string CoordinationOfBenefitsRelatedEntity = "2330";
            public const string AdditionalReportingCategories = "2700";
            public const string MemberReportingCategories = "2710";
            public const string ReportingCategory = "2750";
        }
    }
}