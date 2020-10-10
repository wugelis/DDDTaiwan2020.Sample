using CleanArchitectureCQRSTemplate.Forms;

namespace CleanArchitectureCQRSTemplate
{
    public static class GlobalVar
    {
        public static frmApplicationLayerWizard GetSetApplicationLayerWizardForm { get; set; }

        public static frmGetDtoModel GetDtoModelForm { get; set; }

        public static frmMyORMappingWindow GetSetMyORMMappingWindowForm { get; set; }
    }
}