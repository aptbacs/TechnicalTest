import { FileDetailValidation } from "./file-detail-validation";

export class FileResponse {
  fileName: string = "";
  totalLinesRead: number = 0
  errorMessages: FileDetailValidation[] = [];
}
