/*
* To change this license header, choose License Headers in Project Properties.
* To change this template file, choose Tools | Templates
* and open the template in the editor.
*/
using System;
using iText.Test;

namespace iText.Kernel.Pdf {
    /// <author>benoit</author>
    public class PdfXrefTableTest : ExtendedITextTest {
        public static readonly String sourceFolder = iText.Test.TestUtil.GetParentProjectDirectory(NUnit.Framework.TestContext
            .CurrentContext.TestDirectory) + "/resources/itext/kernel/pdf/PdfXrefTableTest/";

        public static readonly String destinationFolder = NUnit.Framework.TestContext.CurrentContext.TestDirectory
             + "/test/itext/kernel/pdf/PdfXrefTableTest/";

        [NUnit.Framework.OneTimeSetUp]
        public static void BeforeClass() {
            ITextTest.CreateOrClearDestinationFolder(destinationFolder);
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void TestCreateAndUpdateXMP() {
            String created = destinationFolder + "testCreateAndUpdateXMP_create.pdf";
            String updated = destinationFolder + "testCreateAndUpdateXMP_update.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(created));
            pdfDocument.AddNewPage();
            pdfDocument.GetXmpMetadata(true);
            // create XMP metadata
            pdfDocument.Close();
            pdfDocument = new PdfDocument(new PdfReader(created), new PdfWriter(updated));
            pdfDocument.Close();
            pdfDocument = new PdfDocument(new PdfReader(updated));
            PdfXrefTable xref = pdfDocument.GetXref();
            PdfIndirectReference freeRef = xref.Get(xref.Size() - 2);
            // 6
            /*
            Current xref structure:
            xref
            0 8
            0000000006 65535 f % this is object 0; 6 refers to free object 6
            0000000203 00000 n
            0000000510 00000 n
            0000000263 00000 n
            0000000088 00000 n
            0000000015 00000 n
            0000000000 00001 f % this is object 6; 0 refers to free object 0; note generation number
            0000000561 00000 n
            */
            NUnit.Framework.Assert.IsTrue(freeRef.IsFree());
            NUnit.Framework.Assert.AreEqual(xref.Get(0).offsetOrIndex, freeRef.objNr);
            NUnit.Framework.Assert.AreEqual(1, freeRef.genNr);
            pdfDocument.Close();
        }

        /// <exception cref="System.IO.IOException"/>
        [NUnit.Framework.Test]
        public virtual void TestCreateAndUpdateTwiceXMP() {
            String created = destinationFolder + "testCreateAndUpdateTwiceXMP_create.pdf";
            String updated = destinationFolder + "testCreateAndUpdateTwiceXMP_update.pdf";
            String updatedAgain = destinationFolder + "testCreateAndUpdateTwiceXMP_updatedAgain.pdf";
            PdfDocument pdfDocument = new PdfDocument(new PdfWriter(created));
            pdfDocument.AddNewPage();
            pdfDocument.GetXmpMetadata(true);
            // create XMP metadata
            pdfDocument.Close();
            pdfDocument = new PdfDocument(new PdfReader(created), new PdfWriter(updated));
            pdfDocument.Close();
            pdfDocument = new PdfDocument(new PdfReader(updated), new PdfWriter(updatedAgain));
            pdfDocument.Close();
            pdfDocument = new PdfDocument(new PdfReader(updatedAgain));
            PdfXrefTable xref = pdfDocument.GetXref();
            PdfIndirectReference freeRef1 = xref.Get(xref.Size() - 3);
            // 6
            PdfIndirectReference freeRef2 = xref.Get(xref.Size() - 2);
            // 7
            /*
            Current xref structure:
            xref
            0 9
            0000000006 65535 f % this is object 0; 6 refers to free object 6
            0000000203 00000 n
            0000000510 00000 n
            0000000263 00000 n
            0000000088 00000 n
            0000000015 00000 n
            0000000007 00002 f % this is object 6; 7 refers to free object 7; note generation number
            0000000000 00001 f % this is object 7; 0 refers to free object 0; note generation number
            0000000561 00000 n
            */
            NUnit.Framework.Assert.IsTrue(freeRef1.IsFree());
            NUnit.Framework.Assert.AreEqual(xref.Get(0).offsetOrIndex, freeRef1.objNr);
            NUnit.Framework.Assert.AreEqual(2, freeRef1.genNr);
            NUnit.Framework.Assert.IsTrue(freeRef2.IsFree());
            NUnit.Framework.Assert.AreEqual(freeRef1.offsetOrIndex, freeRef2.objNr);
            NUnit.Framework.Assert.AreEqual(1, freeRef2.genNr);
            pdfDocument.Close();
        }
    }
}
