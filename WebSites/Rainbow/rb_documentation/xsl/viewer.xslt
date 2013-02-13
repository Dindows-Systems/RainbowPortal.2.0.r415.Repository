<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt">
	<xsl:output method="xml" version="1.0" indent="no" omit-xml-declaration="yes"/>
	<!-- Rainbow Help Viewer -->
	<!-- Version 1.1 - 2004/10/16 Jes1111 -->
	<!-- TODO: provide localized chrome -->
	<xsl:param name="LanguageRequested">en</xsl:param>
	<xsl:param name="LanguageReturned">en</xsl:param>
	<xsl:param name="Location"/>
	<xsl:param name="AsRequested">true</xsl:param>
	<xsl:param name="Title">Rainbow Help</xsl:param>
	<xsl:param name="myExt">.resx</xsl:param>
	<xsl:param name="myRoot">Rainbow</xsl:param>
	<xsl:param name="TOCfile"/>
	<xsl:param name="Viewer"/>
	<!-- Root match template -->
	<xsl:template match="/">
		<xsl:call-template name="output-structure"/>
	</xsl:template>
	<!-- Output Structure template -->
	<xsl:template name="output-structure">
		<div class="help-popup">
			<xsl:call-template name="top-chrome"/>
			<div class="help-content">
				<xsl:apply-templates select="/dita/topic/title | /dita/topic/shortdesc | /dita/topic/body"/>
			</div>
			<xsl:call-template name="relatedlinks"/>
			<xsl:call-template name="end-chrome"/>
		</div>
	</xsl:template>
	<!-- top chrome -->
	<xsl:template name="top-chrome">
		<div class="top-chrome">
			<p class="title">
				<xsl:value-of select="$Title"/>
			</p>
			<xsl:if test="prolog/metadata/category">
				<p>
					<strong> Category: </strong>
					<xsl:value-of select="prolog/metadata/category"/>
					<xsl:if test="prolog/metadata/prodinfo/component">
						<strong> Component: </strong>
						<xsl:value-of select="prolog/metadata/prodinfo/component"/>
					</xsl:if>
				</p>
			</xsl:if>
			<div class="mainmenu">
				<ul id="topmenu">
					<li class="sub">
						<a href="#">Index</a>
						<xsl:call-template name="navigation"/>
					</li>
				</ul>
			</div>
		</div>
	</xsl:template>
	<!-- navigation -->
	<xsl:template name="navigation">
		<xsl:variable name="TOC" select="document($TOCfile)"/>
		<xsl:apply-templates select="$TOC/*"/>
	</xsl:template>
	<!-- related links -->
	<xsl:template name="relatedlinks">
		<xsl:if test="//related-links">
			<div class="related-links">
				<p style="font-weight:bold;">See also:</p>
				<ul>
					<xsl:for-each select="//related-links/linklist/link">
						<li>
							<xsl:choose>
								<xsl:when test="@loc">
									<xsl:element name="a">
										<xsl:attribute name="href"><xsl:value-of select="$Viewer"/>?loc=<xsl:value-of select="@loc"/>&amp;src=<xsl:value-of select="@src"/></xsl:attribute>
										<xsl:value-of select="linktext"/>
									</xsl:element>
								</xsl:when>
								<xsl:otherwise>
									<xsl:element name="a">
										<xsl:attribute name="href"><xsl:value-of select="@href"/></xsl:attribute>
										<xsl:attribute name="target">_blank</xsl:attribute>
										<xsl:value-of select="linktext"/>
									</xsl:element>
								</xsl:otherwise>
							</xsl:choose>
						</li>
					</xsl:for-each>
				</ul>
			</div>
		</xsl:if>
	</xsl:template>
	<!-- end chrome -->
	<xsl:template name="end-chrome">
		<div class="end-chrome">
			<xsl:for-each select="//prolog/author">
				<p>
					<xsl:value-of select="@type"/>
					<xsl:text>: </xsl:text>
					<xsl:value-of select="."/>
					<xsl:if test="@href">
						<xsl:text> (</xsl:text>
						<xsl:element name="a">
							<xsl:attribute name="href"><xsl:value-of select="@href"/></xsl:attribute>
							<xsl:attribute name="target">_blank</xsl:attribute>
							<xsl:value-of select="@href"/>
						</xsl:element>
						<xsl:text> )</xsl:text>
					</xsl:if>
				</p>
			</xsl:for-each>
			<p>
				<xsl:text>created: </xsl:text>
				<xsl:value-of select="//prolog/critdates/created/@date"/>
			</p>
			<xsl:for-each select="//prolog/critdates/revised">
				<p>
					<xsl:text>revised: </xsl:text>
					<xsl:value-of select="@modified"/>
				</p>
			</xsl:for-each>
		</div>
	</xsl:template>
	<!-- MATCH templates -->
	<!-- section -->
	<xsl:template match="section">
		<xsl:if test="@id">
			<xsl:element name="a">
				<xsl:attribute name="name"><xsl:value-of select="@id"/></xsl:attribute>
			</xsl:element>
		</xsl:if>
		<xsl:apply-templates/>
	</xsl:template>
	<!-- title -->
	<xsl:template match="topic/title">
		<h1>
			<xsl:value-of select="."/>
		</h1>
	</xsl:template>
	<xsl:template match="section/title">
		<h2>
			<xsl:value-of select="."/>
		</h2>
	</xsl:template>
	<!-- short description -->
	<xsl:template match="shortdesc">
		<p class="summary">
			<xsl:value-of select="."/>
		</p>
	</xsl:template>
	<!-- body -->
	<xsl:template match="body">
		<div class="Normal">
			<xsl:apply-templates/>
		</div>
	</xsl:template>
	<!-- paragraph -->
	<xsl:template match="p">
		<p>
			<xsl:apply-templates/>
		</p>
	</xsl:template>
	<!-- simple table -->
	<xsl:template match="simpletable | table">
		<table style="border:1px solid gray;border-collapse:collapse">
			<tbody>
				<xsl:apply-templates/>
			</tbody>
		</table>
	</xsl:template>
	<!-- simple table row -->
	<xsl:template match="strow | tr">
		<tr>
			<xsl:apply-templates/>
		</tr>
	</xsl:template>
	<!-- simple table cell -->
	<xsl:template match="stentry | td">
		<td style="border:1px solid gray">
			<xsl:apply-templates/>
		</td>
	</xsl:template>
	<!-- OASIS table -->
	<!--xsl:template match="table">
		<p style="color:red">OASIS/HTML tables not supported ... use &lt;simpletable&gt;</p>
	</xsl:template-->
	<!-- unordered list -->
	<xsl:template match="ul">
		<ul>
			<xsl:for-each select="li">
				<li>
					<xsl:apply-templates/>
				</li>
			</xsl:for-each>
		</ul>
	</xsl:template>
	<!-- bold -->
	<xsl:template match="b | strong">
		<b>
			<xsl:apply-templates/>
		</b>
	</xsl:template>
	<!-- italic -->
	<xsl:template match="i | em">
		<i>
			<xsl:apply-templates/>
		</i>
	</xsl:template>
	<!-- image -->
	<xsl:template match="image | img">
		<img>
			<xsl:attribute name="src"><xsl:value-of select="$Location"/>/<xsl:value-of select="@href | @src"/></xsl:attribute>
			<xsl:attribute name="alt"><xsl:value-of select="@alt"/></xsl:attribute>
		</img>
	</xsl:template>
	<!-- anchor -->
	<xsl:template match="a">
		<xsl:element name="a">
			<xsl:attribute name="href"><xsl:value-of select="@href"/></xsl:attribute>
			<xsl:attribute name="target">_blank</xsl:attribute>
			<xsl:value-of select="."/>
		</xsl:element>
	</xsl:template>
	<!-- internal anchor (cross reference) -->
	<xsl:template match="xref">
		<xsl:choose>
			<xsl:when test="@href">
				<xsl:element name="a">
					<xsl:attribute name="href"><xsl:value-of select="@href"/></xsl:attribute>
					<xsl:attribute name="target">_blank</xsl:attribute>
					<xsl:value-of select="."/>
				</xsl:element>
			</xsl:when>
			<xsl:otherwise>
				<xsl:element name="a">
					<xsl:attribute name="href"><xsl:value-of select="$Viewer"/>?loc=<xsl:value-of select="@loc"/>&amp;src=<xsl:value-of select="@src"/><xsl:if test="@frag">#<xsl:value-of select="@frag"/></xsl:if></xsl:attribute>
					<xsl:value-of select="."/>
				</xsl:element>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
	<!-- code block -->
	<xsl:template match="codeblock | pre ">
		<pre>
			<xsl:copy-of select="." xml:space="preserve"/>
		</pre>
	</xsl:template>
	<!-- inline code -->
	<xsl:template match="code">
		<code>
			<xsl:copy-of select="." xml:space="preserve"/>
		</code>
	</xsl:template>
	<!-- metadata -->
	<xsl:template match="topicmeta">

	</xsl:template>
	<!-- map -->
	<xsl:template match="map">
		<ul>
			<xsl:apply-templates/>
		</ul>
	</xsl:template>
	<!-- topic reference -->
	<xsl:template match="topicref">
		<xsl:element name="li">
			<xsl:if test="topicref">
				<xsl:attribute name="class">sub</xsl:attribute>
			</xsl:if>
			<xsl:choose>
				<xsl:when test="@href">
					<xsl:element name="a">
						<xsl:attribute name="href"><xsl:value-of select="@href"/></xsl:attribute>
						<xsl:value-of select="@navtitle"/>
					</xsl:element>
				</xsl:when>
				<xsl:otherwise>
					<xsl:choose>
						<xsl:when test="@loc">
							<xsl:element name="a">
								<xsl:attribute name="href"><xsl:value-of select="$Viewer"/>?loc=<xsl:value-of select="$myRoot"/>/<xsl:value-of select="@loc"/>&amp;src=<xsl:value-of select="@src"/></xsl:attribute>
								<xsl:value-of select="@navtitle"/>
							</xsl:element>
						</xsl:when>
						<xsl:otherwise>
							<xsl:element name="a">
								<xsl:attribute name="href">#</xsl:attribute>
								<xsl:attribute name="title">no document</xsl:attribute>
								<i><xsl:value-of select="@navtitle"/></i>
							</xsl:element>
						</xsl:otherwise>
					</xsl:choose>
				</xsl:otherwise>
			</xsl:choose>
			<xsl:if test="topicref">
				<ul>
					<xsl:apply-templates/>
				</ul>
			</xsl:if>
		</xsl:element>
	</xsl:template>
</xsl:stylesheet>
