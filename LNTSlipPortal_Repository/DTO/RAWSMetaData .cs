using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LNTSlipPortal_Repository.Data
{
    [MetadataType(typeof(RAWSMetaData))]
    public partial class RAW
    {
        public string ProjectName { get; set; }
        public string ProductCatNo { get; set; }
        public string ScopeName { get; set; }
        public bool? Grinding { get; set; }
        public bool? PaintTouchUp { get; set; }
        public bool? Replacement { get; set; }
        public bool? Testing { get; set; }
        public bool? Assembly { get; set; }
        public string Other { get; set; }
        public bool? BOM { get; set; }
        public bool? AssemblyList { get; set; }
        public bool? GeneralAssembly { get; set; }
        public bool? TestingDocument { get; set; }
        public bool? WiringScheme { get; set; }
        public bool? ProductData { get; set; }
        public string BOMRevision { get; set; }
        public string AssemblyListRevision { get; set; }
        public string GeneralAssemblyRevision { get; set; }
        public string TestingDocumentRevision { get; set; }
        public string WiringSchemeRevision { get; set; }
        public string ProductDataRevision { get; set; }
        public string BOMCatNo { get; set; }
        public string AssemblyListCatNo { get; set; }
        public string GeneralAssemblyCatNo { get; set; }
        public string TestingDocumentCatNo { get; set; }
        public string WiringSchemeCatNo { get; set; }
        public string ProductDataCatNo { get; set; }
    }
    public class RAWSMetaData
    {
        public string RAWSNO { get; set; }
        [Required(ErrorMessage = "Please select at least one Project.")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Please select at least one Product Category.")]
        public int ProductCatId { get; set; }

        [Required(ErrorMessage = "Please select at least one Scope.")]
        public int ScopeId { get; set; }

        [Required(ErrorMessage = "Please select Work.")]
        public string Work { get; set; }

        [Required(ErrorMessage = "NCR No can't be blank.")]
        public int NCRNo { get; set; }

        [Required(ErrorMessage = "Total Man Hours can't be blank.")]
        public int TotalManHours { get; set; }

        [Required(ErrorMessage = "Total Quantity can't be blank.")]
        public int TotalQuantity { get; set; }
    }

    public partial class RAWDTO : RAW
    {
        public string GrindingLabel { get; set; }
        public string PaintTouchUpLabel { get; set; }
        public string ReplacementLabel { get; set; }
        public string TestingLabel { get; set; }
        public string AssemblyLabel { get; set; }
        public string BOMLabel { get; set; }
        public string AssemblyListLabel { get; set; }
        public string GeneralAssemblyLabel { get; set; }
        public string TestingDocumentLabel { get; set; }
        public string WiringSchemeLabel { get; set; }
        public string ProductDataLabel { get; set; }
        public string JigFixtures_SpecialToolsLabel { get; set; }
        public string InitiatorName { get; set; }
        public string InitiatorPSNumber { get; set; }
        public DateTime? InitiatorCreatedAt { get; set; }
        public string Approved_By_PMG_SCGName { get; set; }
        public string Approved_By_PMG_SCGPSNumber { get; set; }
        public DateTime? Approved_By_PMG_SCGCreatedAt { get; set; }
        public string Acknowlwdge_By_QCName { get; set; }
        public string Acknowlwdge_By_QCPSNumber { get; set; }
        public DateTime? Acknowlwdge_By_QCCreatedAt { get; set; }
        public string Accepted_By_ShopName { get; set; }
        public string Accepted_By_ShopPSNumber { get; set; }
        public DateTime? Accepted_By_ShopCreatedAt { get; set; }
        public string Validated_By_PlannerName { get; set; }
        public string Validated_By_PlannerPSNumber { get; set; }
        public DateTime? Validated_By_PlannerCreatedAt { get; set; }
        public string Checked_Verified_By_ShopName { get; set; }
        public string Checked_Verified_By_ShopPSNumber { get; set; }
        public DateTime? Checked_Verified_By_ShopCreatedAt { get; set; }
        public string Checked_By_QCName { get; set; }
        public string Checked_By_QCPSNumber { get; set; }
        public DateTime? Checked_By_QCCreatedAt { get; set; }

    }
}

