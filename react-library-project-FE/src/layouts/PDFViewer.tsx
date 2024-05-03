import React, { useState } from "react";
import { Document, Page, pdfjs } from "react-pdf";
import "react-pdf/dist/esm/Page/AnnotationLayer.css";

pdfjs.GlobalWorkerOptions.workerSrc = `https://cdnjs.cloudflare.com/ajax/libs/pdf.js/${pdfjs.version}/pdf.worker.min.js`;

const PDFViewer: React.FC<{ urlFile: string | undefined }> = (props) => {
  const [numPages, setNumPages] = useState<number>(0);
  const [pageNumber, setPageNumber] = useState(1);

  const onDocumentLoadSuccess = ({ numPages }: { numPages: number }) => {
    setNumPages(numPages);
  };

  const prevPage = () => {
    setPageNumber(pageNumber - 1 <= 1 ? 1 : pageNumber - 1);
  };
  const nextPage = () => {
    setPageNumber(pageNumber + 1 >= numPages ? pageNumber : pageNumber + 1);
  };
  return (
    <div className="my-3 d-flex align-items-center justify-content-center flex-column">
      <div className="mb-2">
        <i
          className="bi bi-arrow-left-square-fill display-5 text-danger m-3"
          role="button"
          onClick={prevPage}
        ></i>
        <i
          onClick={nextPage}
          className="bi bi-arrow-right-square-fill display-5 text-success poitner"
          role="button"
        ></i>
      </div>
      <div className="d-flex align-items-center justify-content-center border">
        <Document file={props.urlFile} onLoadSuccess={onDocumentLoadSuccess}>
          <Page pageNumber={pageNumber} />
        </Document>
      </div>

      <p>
        Page {pageNumber} of {numPages}
      </p>
      <div className="">
        <i
          className="bi bi-arrow-left-square-fill display-5 text-danger poitner m-3"
          role="button"
          onClick={prevPage}
        ></i>
        <i
          onClick={nextPage}
          className="bi bi-arrow-right-square-fill display-5 text-success poitner"
          role="button"
        ></i>
      </div>
    </div>
  );
};

export default PDFViewer;
