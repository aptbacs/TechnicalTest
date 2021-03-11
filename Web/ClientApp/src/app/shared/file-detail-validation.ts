import { ErrorResponse } from "./error-response.model";

export class FileDetailValidation {
  transactionCode: string = "";
  errors: ErrorResponse = new ErrorResponse();
  lineNumber: number = 0;
}
