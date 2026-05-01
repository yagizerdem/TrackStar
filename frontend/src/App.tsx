import { FullscreenIcon, MinusIcon, XIcon } from "lucide-react";
import { Button } from "./components/ui/button";
import "./title-bar.css";
import {
  ResizableHandle,
  ResizablePanel,
  ResizablePanelGroup,
} from "@/components/ui/resizable";

function App() {
  return (
    <div className="w-screen h-screen  flex flex-col">
      <div className="w-full h-12 bg-secondary titlebar flex flex-row justify-between align-middle items-center">
        <div></div>
        <div className="flex flex-row ">
          <Button className="no-drag">
            <MinusIcon />
          </Button>
          <Button className="no-drag">
            <FullscreenIcon />
          </Button>
          <Button className="no-drag">
            <XIcon />
          </Button>
        </div>
      </div>
      <div className="w-full h-full">
        <ResizablePanelGroup
          orientation="horizontal"
          className="w-full rounded-lg border"
        >
          <ResizablePanel defaultSize="20%" maxSize={300} minSize={0}>
            <div className="flex h-full items-center justify-center p-6 bg-secondary">
              <span className="font-semibold">One</span>
            </div>
          </ResizablePanel>
          <ResizableHandle withHandle />
          <ResizablePanel defaultSize="80%">
            <div>alfj</div>
          </ResizablePanel>
        </ResizablePanelGroup>
      </div>
    </div>
  );
}

export default App;
