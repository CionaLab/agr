% This code takes an individual TIFF stack and outputs a series of CSV files, each representing individual slices.
% It can handle up to 4 channels as long and by default name them as red, green, blue, and white (in that order)

slices = 25;
width = 1012;
height = 424;
redT = 0; %T is for the threshold of that channel
greenT = 120;
blueT = 120;
whiteT = 206;

% This part takes a maximum projection image of TIFF stack and determines
% the pixels that are higher than the given threshold
[dataFile,dataDir] = uigetfile('*.tif','Select the TIFF file to import','MultiSelect', 'off');
iIndex = imread([dataDir dataFile]);
indices = iIndex > redT;
% This part goes slice by slice and gets the voxel value within the areas
% of interest determined form the maximum projection
[dataFile,dataDir] = uigetfile('*.tif','Select the TIFF file to import','MultiSelect', 'off');
firstHalf = 'red';
secondHalf = '.csv';
for i = 1:slices
    newName = strcat(firstHalf,num2str(i),secondHalf);
    iTemp = imread([dataDir dataFile],i);
    iNew = zeros(height,width);
    for j = 1:height
        for k = 1:width
            if (indices(j,k) > 0) && (iTemp(j,k) > redT)
                iNew(j,k) = iTemp(j,k);
            end
        end
    end
    writematrix(iNew,newName);
end

% This just repeats the same thing, but makes the next channel. This is
% separate for the sole purpose of ease of commenting out channels not
% wanting to be used
[dataFile,dataDir] = uigetfile('*.tif','Select the TIFF file to import','MultiSelect', 'off');
iIndex = imread([dataDir dataFile]);
indices = iIndex > greenT;
[dataFile,dataDir] = uigetfile('*.tif','Select the TIFF file to import','MultiSelect', 'off');
firstHalf = 'green';
secondHalf = '.csv';
for i = 1:slices
    newName = strcat(firstHalf,num2str(i),secondHalf);
    iTemp = imread([dataDir dataFile],i);
    iNew = zeros(height,width);
    for j = 1:height
        for k = 1:width
            if (indices(j,k) > 0) && (iTemp(j,k) > greenT)
                iNew(j,k) = iTemp(j,k);
            end
        end
    end
    writematrix(iNew,newName);
end


[dataFile,dataDir] = uigetfile('*.tif','Select the TIFF file to import','MultiSelect', 'off');
iIndex = imread([dataDir dataFile]);
indices = iIndex > blueT;
[dataFile,dataDir] = uigetfile('*.tif','Select the TIFF file to import','MultiSelect', 'off');
firstHalf = 'blue';
secondHalf = '.csv';
for i = 1:slices
    newName = strcat(firstHalf,num2str(i),secondHalf);
    iTemp = imread([dataDir dataFile],i);
    iNew = zeros(height,width);
    for j = 1:height
        for k = 1:width
            if (indices(j,k) > 0) && (iTemp(j,k) > blueT)
                iNew(j,k) = iTemp(j,k);
            end
        end
    end
    writematrix(iNew,newName);
end

[dataFile,dataDir] = uigetfile('*.tif','Select the TIFF file to import','MultiSelect', 'off');
iIndex = imread([dataDir dataFile]);
indices = iIndex > 254;
[dataFile,dataDir] = uigetfile('*.tif','Select the TIFF file to import','MultiSelect', 'off');
firstHalf = 'white';
secondHalf = '.csv';
for i = 1:slices
    newName = strcat(firstHalf,num2str(i),secondHalf);
    iTemp = imread([dataDir dataFile],i);
    iNew = zeros(height,width);
    for j = 1:height
        for k = 1:width
            if (indices(j,k) > 0) && (iTemp(j,k) > 254)
                iNew(j,k) = 255;
            end
        end
    end
    writematrix(iNew,newName);
end
